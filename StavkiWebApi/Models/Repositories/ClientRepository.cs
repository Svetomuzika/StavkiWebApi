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
            return DBContext.Clients.Include(x => x.Requests);
        }

        public void Add(Client client)
        {
            DBContext.Clients.Add(client);
            DBContext.SaveChanges();
        }

        public bool CreateAccount(Client client)
        {
            if(GetAll().Any(x => x.Login == client.Login))
                return false;

            try
            {
                DBContext.Clients.Add(client);
                DBContext.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Update(Client client)
        {
            DBContext.Entry(client).State = EntityState.Modified;
            DBContext.SaveChanges();
        }

        public void Delete(Client item)
        {
            DBContext.Clients.Remove(item);
            DBContext.SaveChanges();
        }

        public Client Auth(string data)
        {
            var login = data.Split(new char[] { '/' }).First();
            var password = data.Split(new char[] { '/' }).Last();

            return GetAll().Where(x => x.Login == login && x.Password == password).SingleOrDefault();
        }

        public Client GetById(int id)
        {
            return DBContext.Clients.Include(x => x.Requests).Where(x => x.Id == id).SingleOrDefault();
        }
    }
}
