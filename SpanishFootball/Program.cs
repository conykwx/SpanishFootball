using SpanishFootball.Models;
using TournamentApp.Data;
using TournamentApp.Services;

class Program
{
    static void Main(string[] args)
    {
        var options = new string[]
        {
            "Показать все команды",
            "Добавить команду",
            "Изменить команду по ID",
            "Удалить команду по ID",
            "Показать команду с максимальными победами",
            "Показать команду с максимальными поражениями",
            "Показать команду с максимальными ничьими",
            "Показать команду с максимальными забитыми голами",
            "Показать команду с максимальными пропущенными голами",
            "Выход"
        };

        var context = new TournamentContext(); // Подключение к базе данных
        var service = new TeamService(context);

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Меню:");
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            Console.Write("\nВыберите опцию: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowTeams(service);
                        break;
                    case 2:
                        AddTeam(service);
                        break;
                    case 3:
                        UpdateTeam(service);
                        break;
                    case 4:
                        DeleteTeam(service);
                        break;
                    case 5:
                        ShowTeamWithMostWins(service);
                        break;
                    case 6:
                        ShowTeamWithMostLosses(service);
                        break;
                    case 7:
                        ShowTeamWithMostDraws(service);
                        break;
                    case 8:
                        ShowTeamWithMostGoalsScored(service);
                        break;
                    case 9:
                        ShowTeamWithMostGoalsConceded(service);
                        break;
                    case 10:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
            }
        }
    }

    static void ShowTeams(TeamService service)
    {
        var teams = service.GetAllTeams();
        if (teams.Any())
        {
            Console.Clear();
            Console.WriteLine("Турнирная таблица:");
            foreach (var team in teams)
            {
                Console.WriteLine($"{team.Id}: {team.Name} ({team.City})");
            }
        }
        else
        {
            Console.WriteLine("Нет команд.");
        }
        Console.WriteLine("\nНажмите любую клавишу для возвращения в меню...");
        Console.ReadKey();
    }

    static void AddTeam(TeamService service)
    {
        Console.Clear();
        Console.WriteLine("Добавление новой команды:");

        var team = new Team();
        Console.Write("Название команды: ");
        team.Name = Console.ReadLine();
        Console.Write("Город команды: ");
        team.City = Console.ReadLine();
        Console.Write("Количество побед: ");
        team.Wins = int.Parse(Console.ReadLine());
        Console.Write("Количество поражений: ");
        team.Losses = int.Parse(Console.ReadLine());
        Console.Write("Количество ничьих: ");
        team.Draws = int.Parse(Console.ReadLine());
        Console.Write("Забитые голы: ");
        team.GoalsScored = int.Parse(Console.ReadLine());
        Console.Write("Пропущенные голы: ");
        team.GoalsConceded = int.Parse(Console.ReadLine());

        if (service.AddTeam(team))
        {
            Console.WriteLine("Команда добавлена.");
        }
        else
        {
            Console.WriteLine("Команда с таким названием и городом уже существует.");
        }
        Console.WriteLine("\nНажмите любую клавишу для возвращения в меню...");
        Console.ReadKey();
    }

    static void UpdateTeam(TeamService service)
    {
        Console.Clear();
        Console.WriteLine("Изменение данных команды:");
        Console.Write("Введите ID команды для изменения: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var team = service.FindById(id);
            if (team != null)
            {
                Console.WriteLine($"Вы выбрали команду: {team.Name} ({team.City})");
                var updatedTeam = new Team { Id = id };
                Console.Write("Новое название команды: ");
                updatedTeam.Name = Console.ReadLine();
                Console.Write("Новый город команды: ");
                updatedTeam.City = Console.ReadLine();
                Console.Write("Количество побед: ");
                updatedTeam.Wins = int.Parse(Console.ReadLine());
                Console.Write("Количество поражений: ");
                updatedTeam.Losses = int.Parse(Console.ReadLine());
                Console.Write("Количество ничьих: ");
                updatedTeam.Draws = int.Parse(Console.ReadLine());
                Console.Write("Количество забитых голов: ");
                updatedTeam.GoalsScored = int.Parse(Console.ReadLine());
                Console.Write("Количество пропущенных голов: ");
                updatedTeam.GoalsConceded = int.Parse(Console.ReadLine());

                service.UpdateTeam(id, updatedTeam);
                Console.WriteLine("Данные команды успешно обновлены.");
            }
            else
            {
                Console.WriteLine("Команда с таким ID не найдена.");
            }
        }
        else
        {
            Console.WriteLine("Неверный ввод ID.");
        }
        Console.WriteLine("\nНажмите любую клавишу для возвращения в меню...");
        Console.ReadKey();
    }

    static void DeleteTeam(TeamService service)
    {
        Console.Clear();
        Console.WriteLine("Удаление команды:");
        Console.Write("Введите ID команды для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (service.DeleteTeam(id))
            {
                Console.WriteLine("Команда удалена.");
            }
            else
            {
                Console.WriteLine("Команда с таким ID не найдена.");
            }
        }
        else
        {
            Console.WriteLine("Неверный ввод ID.");
        }
        Console.WriteLine("\nНажмите любую клавишу для возвращения в меню...");
        Console.ReadKey();
    }

    static void ShowTeamWithMostWins(TeamService service) =>
        ShowTeam(service.GetTeamWithMostWins(), "побед");

    static void ShowTeamWithMostLosses(TeamService service) =>
        ShowTeam(service.GetTeamWithMostLosses(), "поражений");

    static void ShowTeamWithMostDraws(TeamService service) =>
        ShowTeam(service.GetTeamWithMostDraws(), "ничьих");

    static void ShowTeamWithMostGoalsScored(TeamService service) =>
        ShowTeam(service.GetTeamWithMostGoalsScored(), "забитых голов");

    static void ShowTeamWithMostGoalsConceded(TeamService service) =>
        ShowTeam(service.GetTeamWithMostGoalsConceded(), "пропущенных голов");

    static void ShowTeam(Team? team, string category)
    {
        if (team != null)
        {
            Console.WriteLine($"Команда с наибольшим количеством {category}: {team.Name} ({team.City})");
        }
        else
        {
            Console.WriteLine("Нет команд с таким показателем.");
        }
        Console.WriteLine("\nНажмите любую клавишу для возвращения в меню...");
        Console.ReadKey();
    }
}
