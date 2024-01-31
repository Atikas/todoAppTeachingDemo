using Microsoft.EntityFrameworkCore;
using TodoApp.DAL.Entities;

namespace TodoApp.DAL
{
    public class TodoAppContext: DbContext
    {
        public TodoAppContext(DbContextOptions<TodoAppContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<TodoItem> TodoItems { get; set; } = null!;

    }
}
