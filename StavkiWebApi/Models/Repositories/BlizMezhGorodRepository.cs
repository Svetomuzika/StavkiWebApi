using StavkiWebApi.Models.EF;
using StavkiWebApi.Models.Entites;
using StavkiWebApi.Models.Interfaces;

namespace StavkiWebApi.Models.Repositories
{
    public class BlizMezhGorodRepository : IRepository<BlizMezhGorodSNDS>
    {
        private ApplicationContext DBContext;

        public BlizMezhGorodRepository(ApplicationContext context)
        {
            DBContext = context;
        }

        public IEnumerable<BlizMezhGorodSNDS> GetAll()
        {
            return DBContext.BlizMezhGorodSNDS;
        }

        public BlizMezhGorodSNDS Auth(string data)
        {
            throw new NotImplementedException();
        }

        public void Add(BlizMezhGorodSNDS item)
        {
            throw new NotImplementedException();
        }

        public bool CreateAccount(Client item)
        {
            throw new NotImplementedException();
        }

        public void Delete(BlizMezhGorodSNDS item)
        {
            throw new NotImplementedException();
        }

        public void Update(BlizMezhGorodSNDS item)
        {
            throw new NotImplementedException();
        }

        public BlizMezhGorodSNDS GetById(int id)
        {
            return DBContext.BlizMezhGorodSNDS.Find(id);
        }
    }
}
