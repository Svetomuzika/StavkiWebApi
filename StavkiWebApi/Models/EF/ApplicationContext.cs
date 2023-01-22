using Microsoft.EntityFrameworkCore;
using StavkiWebApi.Models.Entites;

namespace StavkiWebApi.Models.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Request> Requests => Set<Request>();
        public DbSet<Gorod> Gorod => Set<Gorod>();
        public DbSet<BlizMezhGorodSNDS> BlizMezhGorodSNDS => Set<BlizMezhGorodSNDS>();
        public DbSet<MezhgorodSNDS> MezhgorodSNDS => Set<MezhgorodSNDS>();

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=" + Environment.CurrentDirectory + @"\StvavkiDB.db");
        }
    }
}
