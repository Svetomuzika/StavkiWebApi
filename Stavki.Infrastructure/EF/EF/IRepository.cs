using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stavki.Infrastructure.EF.EF
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity FindById(int id);
        List<TEntity> Get();
        void Update(TEntity item);
        List<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity item);

    }
}
