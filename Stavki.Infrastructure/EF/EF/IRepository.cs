using System.Linq.Expressions;

namespace Stavki.Infrastructure.EF.EF
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity FindById(int id);
        List<TEntity> Get();
        List<TEntity> GetNotDeleted();
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);
        void Update(TEntity item);
        List<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity item);
    }
}
