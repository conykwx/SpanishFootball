using Microsoft.EntityFrameworkCore;
using SpanishFootball.Models;

namespace TournamentApp.Data
{
    public class TournamentContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-9K56BQI\\SQLEXPRESS;Database=TournamentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}