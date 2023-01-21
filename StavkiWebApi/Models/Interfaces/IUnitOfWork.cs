using StavkiWebApi.Models.Entites;

namespace StavkiWebApi.Models.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Client> Clients { get; }

        IRepository<Gorod> Gorod { get; }
        IRepository<BlizMezhGorodSNDS> BlizMezhGorodSNDS { get; }
        IRepository<MezhgorodSNDS> MezhgorodSNDS { get; }
        IRepository<Request> Requests { get; }
        void Save();
    }
}
