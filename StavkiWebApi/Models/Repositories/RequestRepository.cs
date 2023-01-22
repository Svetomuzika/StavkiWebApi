using Microsoft.EntityFrameworkCore;
using StavkiWebApi.Models.EF;
using StavkiWebApi.Models.Entites;
using StavkiWebApi.Models.Interfaces;

namespace StavkiWebApi.Models.Repositories
{
    public class RequestRepository : IRepository<Request>
    {
        private ApplicationContext DBContext;

        public RequestRepository(ApplicationContext context)
        {
            DBContext = context;
        }

        public IEnumerable<Request> GetAll()
        {
            return DBContext.Requests;
        }

        public void Add(Request item)
        {
            DBContext.Requests.Add(item);
            DBContext.SaveChanges();
        }

        public void Update(Request item)
        {
            DBContext.Entry(item).State = EntityState.Modified;
            DBContext.SaveChanges();
        }

        public void Delete(Request item)
        {
            DBContext.Requests.Remove(item);
            DBContext.SaveChanges();
        }

        bool IRepository<Request>.CreateAccount(Client item)
        {
            throw new NotImplementedException();
        }

        Request IRepository<Request>.Auth(string data)
        {
            throw new NotImplementedException();
        }
    }
}
