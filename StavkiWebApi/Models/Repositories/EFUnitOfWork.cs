using StavkiWebApi.Models.EF;
using StavkiWebApi.Models.Entites;
using StavkiWebApi.Models.Interfaces;

namespace StavkiWebApi.Models.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext DBContext;
        private ClientRepository clientRepository;
        private GorodRepositorty gorodRepository;
        private BlizMezhGorodRepository blizMezhGorodSNDSRepository;
        private MezhGorodRepository mezhgorodSNDSRepository;
        private RequestRepository requestRepository;

        public EFUnitOfWork(ApplicationContext context)
        {
            DBContext = context;
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(DBContext);
                return clientRepository;
            }
        }

        public IRepository<Gorod> Gorod
        {
            get
            {
                if (gorodRepository == null)
                    gorodRepository = new GorodRepositorty(DBContext);

                return gorodRepository;
            }
        }

        public IRepository<BlizMezhGorodSNDS> BlizMezhGorodSNDS
        {
            get
            {
                if (blizMezhGorodSNDSRepository == null)
                    blizMezhGorodSNDSRepository = new BlizMezhGorodRepository(DBContext);

                return blizMezhGorodSNDSRepository;
            }
        }

        public IRepository<MezhgorodSNDS> MezhgorodSNDS
        {
            get
            {
                if (mezhgorodSNDSRepository == null)
                    mezhgorodSNDSRepository = new MezhGorodRepository(DBContext);

                return mezhgorodSNDSRepository;
            }
        }

        public IRepository<Request> Requests
        {
            get
            {
                if (requestRepository == null)
                    requestRepository = new RequestRepository(DBContext);

                return requestRepository;
            }
        }

        public void Save()
        {
            DBContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DBContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
