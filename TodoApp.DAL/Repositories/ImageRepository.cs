using Microsoft.EntityFrameworkCore;
using TodoApp.DAL.Entities;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.DAL.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(TodoAppContext context) : base(context)
        {
        }
        override public IQueryable<Image> GetAll()
        {
            return _context.Images.Include(i => i.TodoItem);
        }
        public override Image? Get(long id)
        {
            return _context.Images.Include(i => i.TodoItem).FirstOrDefault(i => i.Id == id);
        }
    }
}
