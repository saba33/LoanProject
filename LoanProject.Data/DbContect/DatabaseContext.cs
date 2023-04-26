using LoanProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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

    }
}
