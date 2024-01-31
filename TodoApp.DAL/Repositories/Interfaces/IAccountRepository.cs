using TodoApp.DAL.Entities;

namespace TodoApp.DAL.Repositories.Interfaces;

public interface IAccountRepository
{
    Guid Create(Account model);
    Account? Get(string userName);
}
