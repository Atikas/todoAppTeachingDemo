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
        override public  TodoItem? Get(long id)
        {
            return _context.TodoItems.Include(i => i.Images).Include(i => i.Place).FirstOrDefault(x => x.Id == id);
        }
    }
}
