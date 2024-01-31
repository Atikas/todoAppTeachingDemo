using TodoApp.DAL.Entities;

namespace TodoApp.DAL.Repositories.Interfaces;

public interface IAccountRepository
{
    void Create(Account model);
    Account? Get(string userName);
}
