namespace TodoApp.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T? Get(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
