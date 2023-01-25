using Microsoft.EntityFrameworkCore;
using StavkiWebApi.Models.EF;
using StavkiWebApi.Models.Entites;
using StavkiWebApi.Models.Interfaces;

namespace StavkiWebApi.Models.Repositories
{
    public class GorodRepositorty : IRepository<Gorod>
    {
        private ApplicationContext DBContext;

        public GorodRepositorty(ApplicationContext context)
        {
            DBContext = context;
        }

        public IEnumerable<Gorod> GetAll()
        {
            return DBContext.Gorod;
        }

        public Gorod Auth(string data)
        {
            throw new NotImplementedException();
        }

        public void Add(Gorod item)
        {
            throw new NotImplementedException();
        }

        public bool CreateAccount(Client item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Gorod item)
        {
            throw new NotImplementedException();
        }

        public void Update(Gorod item)
        {
            throw new NotImplementedException();
        }

        public Gorod GetById(int id)
        {
            return DBContext.Gorod.Find(id);
        }
    }
}