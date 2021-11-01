namespace Auditorium
{
    using System.IO;
    using Auditorium.Entities;
    using Microsoft.EntityFrameworkCore;
    using Xamarin.Essentials;

    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppContext()
        {
            CreateDbIfNotExist();
        }

        private async void CreateDbIfNotExist()
        {
            await Database.EnsureCreatedAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Auditorium.db");
            
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}