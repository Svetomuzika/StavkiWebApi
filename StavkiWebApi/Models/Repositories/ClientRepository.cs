using Microsoft.EntityFrameworkCore;
using StavkiWebApi.Models.EF;
using StavkiWebApi.Models.Entites;
using StavkiWebApi.Models.Interfaces;

namespace StavkiWebApi.Models.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private ApplicationContext DBContext;

        public ClientRepository(ApplicationContext context)
        {
            DBContext = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return DBContext.Clients;
        }

        public void Create(Client client)
        {
            DBContext.Clients.Add(client);
            DBContext.SaveChanges();
        }

        public void Update(Client client)
        {
            DBContext.Entry(client).State = EntityState.Modified;
            DBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Client client = DBContext.Clients.Find(id);

            if (client != null)
            {
                DBContext.Clients.Remove(client);
                DBContext.SaveChanges();
            }
        }

        public Client Auth(string data)
        {
            var login = data.Split(new char[] { '/' }).First();
            var password = data.Split(new char[] { '/' }).Last();

            return GetAll().Where(x => x.Login == login && x.Password == password).SingleOrDefault();
        }
    }
}
