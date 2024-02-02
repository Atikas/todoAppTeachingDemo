using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {

        protected readonly TodoAppContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(TodoAppContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
            _dbSet = context.Set<T>();
        }
        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual T? Get(long id)
        {
            return _dbSet.Find(id);
        }
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
