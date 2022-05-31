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

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost;database=BookStoreDb;uid=root;password=;");
        }*/

        public DbSet<EmailData> EmailData { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EmailData>().HasKey(p => p.Key);
        }
    }
}
