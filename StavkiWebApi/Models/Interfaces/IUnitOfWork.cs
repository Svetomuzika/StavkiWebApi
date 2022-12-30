using StavkiWebApi.Models.Entites;

namespace StavkiWebApi.Models.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Client> Clients { get; }
        void Save();
    }
}
