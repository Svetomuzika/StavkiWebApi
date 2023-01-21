using StavkiWebApi.Models.EF;
using StavkiWebApi.Models.Entites;
using StavkiWebApi.Models.Interfaces;

namespace StavkiWebApi.Models.Repositories
{
    public class MezhgorodRepository : IRepository<MezhgorodSNDS>
    {

        private ApplicationContext DBContext;

        public MezhgorodRepository(ApplicationContext context)
        {
            DBContext = context;
        }

        public IEnumerable<MezhgorodSNDS> GetAll()
        {
            return DBContext.MezhgorodSNDS;
        }

        MezhgorodSNDS IRepository<MezhgorodSNDS>.Auth(string data)
        {
            throw new NotImplementedException();
        }

        void IRepository<MezhgorodSNDS>.Create(MezhgorodSNDS item)
        {
            throw new NotImplementedException();
        }

        bool IRepository<MezhgorodSNDS>.CreateAccount(Client item)
        {
            throw new NotImplementedException();
        }

        void IRepository<MezhgorodSNDS>.Delete(MezhgorodSNDS item)
        {
            throw new NotImplementedException();
        }

        void IRepository<MezhgorodSNDS>.Update(MezhgorodSNDS item)
        {
            throw new NotImplementedException();
        }
    }
}
