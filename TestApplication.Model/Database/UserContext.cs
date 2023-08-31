using Microsoft.EntityFrameworkCore;
using TestApplication.Model.Database.Entities;

namespace TestApplication.Model.Database
{
    public class UserContext : DbContext
    {
        public UserContext()
        {
        }
        public UserContext(DbContextOptions<UserContext> options)
          : base(options)
        {

        }
        public UserContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

       
        public DbSet<User>User { get; set; }
        public DbSet<Role> Role { get; set; }

        public DbSet<UserRole> UserRole { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              //  optionsBuilder.UseNpgsql("Host=hrtrgnew-db.cp3eirt92hau.ap-southeast-1.rds.amazonaws.com;Database=ilearn;Username=masterUser123;Password=masterpassword", x => x.UseNodaTime());
            }
        }
    }
}
