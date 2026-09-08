using AssessmentTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssessmentTest.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        public DbSet<FitnessCoach> Coaches => Set<FitnessCoach>();

        public DbSet<Client> Clients => Set<Client>();

        public DbSet<Plan> Plans => Set<Plan>();
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
