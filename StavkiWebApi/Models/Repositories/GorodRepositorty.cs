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

        Gorod IRepository<Gorod>.Auth(string data)
        {
            throw new NotImplementedException();
        }

        void IRepository<Gorod>.Create(Gorod item)
        {
            throw new NotImplementedException();
        }

        bool IRepository<Gorod>.CreateAccount(Client item)
        {
            throw new NotImplementedException();
        }

        void IRepository<Gorod>.Delete(Gorod item)
        {
            throw new NotImplementedException();
        }

        void IRepository<Gorod>.Update(Gorod item)
        {
            throw new NotImplementedException();
        }
    }
}