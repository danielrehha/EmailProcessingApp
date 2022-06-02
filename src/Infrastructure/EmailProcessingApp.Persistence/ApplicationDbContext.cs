using EmailProcessingApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmailProcessingApp.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<EmailData> EmailData { get; set; }
        public DbSet<ResponseEmail> ResponseEmail { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EmailData>().HasKey(p => p.Key);
            builder.Entity<ResponseEmail>().HasKey(p => p.Key);
        }
    }
}
