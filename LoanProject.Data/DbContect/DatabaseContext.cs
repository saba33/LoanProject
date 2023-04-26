using LoanProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanProject.Data.DbContect
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Loan>()
                   .HasOne(l => l.User)
                   .WithMany(u => u.Loans)
                   .HasForeignKey(l => l.UserId);

            base.OnModelCreating(builder);
        }

        public void SeedData()
        {
            if (Users.Any())
            {
                return;
            }

            var adminUser = new User
            {
                Name = "Saba",
                LastName = "Gabedava",
                Email = "gabedavasaba5@gmail.com",
                PasswordHash = null, 
                PasswordSalt = null,
                IdNumber = "12398745612",
                Role = "Admin",
                DateOfBirth = new DateTime(2000, 1, 1),
                Loans = new List<Loan>()
            };

            Users.Add(adminUser);
            SaveChanges();
        }

    }
}
