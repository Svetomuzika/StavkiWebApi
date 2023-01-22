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

        BlizMezhGorodSNDS IRepository<BlizMezhGorodSNDS>.Auth(string data)
        {
            throw new NotImplementedException();
        }

        void IRepository<BlizMezhGorodSNDS>.Add(BlizMezhGorodSNDS item)
        {
            throw new NotImplementedException();
        }

        bool IRepository<BlizMezhGorodSNDS>.CreateAccount(Client item)
        {
            throw new NotImplementedException();
        }

        void IRepository<BlizMezhGorodSNDS>.Delete(BlizMezhGorodSNDS item)
        {
            throw new NotImplementedException();
        }

        void IRepository<BlizMezhGorodSNDS>.Update(BlizMezhGorodSNDS item)
        {
            throw new NotImplementedException();
        }
    }
}
