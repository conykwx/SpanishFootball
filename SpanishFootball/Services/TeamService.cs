using SpanishFootball.Models;
using TournamentApp.Data;

namespace TournamentApp.Services
{
    public class TeamService
    {
        private readonly TournamentContext _context;

        public TeamService(TournamentContext context)
        {
            _context = context;
        }

        public List<Team> GetAllTeams()
        {
            return _context.Teams.ToList(); // загружаем все команды
        }

        // поиск команды по ID
        public Team? FindById(int id) =>
            _context.Teams.FirstOrDefault(t => t.Id == id);

        // поиск команды по названию
        public Team? FindByName(string name) =>
            _context.Teams.FirstOrDefault(t => t.Name == name);

        // поиск команд по городу
        public List<Team> FindByCity(string city) =>
            _context.Teams.Where(t => t.City == city).ToList();

        // поиск команды по названию и городу
        public Team? FindByNameAndCity(string name, string city) =>
            _context.Teams.FirstOrDefault(t => t.Name == name && t.City == city);

        // команда с максимальными победами
        public Team? GetTeamWithMostWins() =>
            _context.Teams.OrderByDescending(t => t.Wins).FirstOrDefault();

        // команда с максимальными поражениями
        public Team? GetTeamWithMostLosses() =>
            _context.Teams.OrderByDescending(t => t.Losses).FirstOrDefault();

        // команда с максимальными ничьими
        public Team? GetTeamWithMostDraws() =>
            _context.Teams.OrderByDescending(t => t.Draws).FirstOrDefault();

        // команда с максимальными забитыми голами
        public Team? GetTeamWithMostGoalsScored() =>
            _context.Teams.OrderByDescending(t => t.GoalsScored).FirstOrDefault();

        // команда с максимальными пропущенными голами
        public Team? GetTeamWithMostGoalsConceded() =>
            _context.Teams.OrderByDescending(t => t.GoalsConceded).FirstOrDefault();

        // добавление новой команды
        public bool AddTeam(Team team)
        {
            if (_context.Teams.Any(t => t.Name == team.Name && t.City == team.City))
                return false;

            _context.Teams.Add(team);
            _context.SaveChanges();
            return true;
        }

        // изменение данных команды
        public void UpdateTeam(int id, Team updatedTeam)
        {
            var team = FindById(id);
            if (team != null)
            {
                team.Name = updatedTeam.Name;
                team.City = updatedTeam.City;
                team.Wins = updatedTeam.Wins;
                team.Losses = updatedTeam.Losses;
                team.Draws = updatedTeam.Draws;
                team.GoalsScored = updatedTeam.GoalsScored;
                team.GoalsConceded = updatedTeam.GoalsConceded;

                _context.SaveChanges();
            }
        }

        // удаление команды
        public bool DeleteTeam(int id)
        {
            var team = FindById(id);
            if (team == null) return false;

            _context.Teams.Remove(team);
            _context.SaveChanges();
            return true;
        }
    }
}
