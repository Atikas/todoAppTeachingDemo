using TodoApp.DAL.Entities;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.DAL.Repositories;
public class AccountRepository : IAccountRepository
{
    private readonly TodoAppContext _context;

    public AccountRepository(TodoAppContext context)
    {
        _context = context;
    }

    public void Create(Account model)
    {
        _context.Accounts.Add(model);
        _context.SaveChanges();
    }
    public Account? Get(string userName)
    {
        return _context.Accounts.FirstOrDefault(x => x.UserName == userName);
    }
}
