using Microsoft.EntityFrameworkCore;
using TodoApp.DAL.Entities;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.DAL.Repositories
{
    public class TodoRepository : Repository<TodoItem>, ITodoRepository
    {
        public TodoRepository(TodoAppContext context) : base(context)
        {
        }
        override public IQueryable<TodoItem> GetAll()
        {
            return _context.TodoItems.Include(x => x.Images);
        }
        override public  TodoItem? Get(long id)
        {
            return _context.TodoItems.Include(x => x.Images).FirstOrDefault(x => x.Id == id);
        }
    }
}
