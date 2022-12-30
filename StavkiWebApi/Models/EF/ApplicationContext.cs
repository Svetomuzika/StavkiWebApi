using Microsoft.EntityFrameworkCore;
using StavkiWebApi.Models.Entites;

namespace StavkiWebApi.Models.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Console.WriteLine("Sdds");

            Database.Migrate();
        }
    }
}
