using Microsoft.EntityFrameworkCore;

namespace TestBackend.Api.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestBackend;Trusted_Connection=True;");
            }
        }
    }
}
