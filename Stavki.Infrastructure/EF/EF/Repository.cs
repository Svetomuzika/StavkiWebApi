using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Stavki.Infrastructure.EF.EF
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DbContext _context;
        DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public List<TEntity> Get() => _dbSet.AsNoTracking().ToList();

        public List<TEntity> Get(Expression<Func<TEntity, bool>> predicate) => _dbSet.AsNoTracking().Where(predicate).ToList();

        public TEntity FindById(int id) => _dbSet.Find(id);

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
