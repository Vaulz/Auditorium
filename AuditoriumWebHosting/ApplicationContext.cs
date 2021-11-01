namespace AuditoriumWebHosting
{
    using AuditoriumWebHosting.Entities;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationContext : DbContext
    {
        public DbSet<AccountData> AccountDatas { get; set; }
        
        public DbSet<Auditory> Auditories { get; set; }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<KeyJournal> KeyJournals { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Auditorium.db");
        }
    }
}