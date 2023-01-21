using StavkiWebApi.Models.Entites;

namespace StavkiWebApi.Models.Interfaces
{
    public interface IRepository<T> where T :  class
    {
        IEnumerable<T> GetAll();

        void Create(T item);

        bool CreateAccount(Client item);

        void Update(T item);

        void Delete(T item);

        T Auth(string data);
    }
}
