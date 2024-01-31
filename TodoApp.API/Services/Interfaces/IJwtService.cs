using TodoApp.DAL.Entities;

namespace TodoApp.API.Services.Interfaces;

public interface IJwtService
{
    string GetJwtToken(Account account);
}
