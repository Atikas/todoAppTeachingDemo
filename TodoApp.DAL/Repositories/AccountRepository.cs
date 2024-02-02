using TodoApp.DAL.Entities;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.DAL.Repositories;
public class AccountRepository : IAccountRepository
{
    private readonly TodoAppContext _context;

    public AccountRepository(TodoAppContext context)
    {
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        _context = context;
    }

    public Guid Create(Account model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        
        var exists = _context.Accounts.Any(x => x.UserName == model.UserName);
        if (exists)
            throw new ArgumentException("Username already exists");
        
        _context.Accounts.Add(model);
        _context.SaveChanges();
        return model.Id;
    }
    public Account? Get(string userName)
    {
        if (userName == null)
               throw new ArgumentNullException(nameof(userName));
        
        return _context.Accounts.FirstOrDefault(x => x.UserName == userName);
    }
    public bool Exists(Guid id)
    {
        return _context.Accounts.Any(x => x.Id == id);
    }
    public void Delete(Guid id)
    {
        var account = _context.Accounts.Find(id);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
