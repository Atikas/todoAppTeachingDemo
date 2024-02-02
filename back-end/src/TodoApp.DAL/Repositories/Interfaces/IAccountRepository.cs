using TodoApp.DAL.Entities;

namespace TodoApp.DAL.Repositories.Interfaces;

public interface IAccountRepository
{
    Guid Create(Account model);
    void Delete(Guid id);
    bool Exists(Guid id);
    Account? Get(string userName);
}
