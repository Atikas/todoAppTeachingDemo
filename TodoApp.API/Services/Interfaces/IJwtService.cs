namespace TodoApp.API.Services.Interfaces;

//------------------------------------------------------------------------------------
public interface IJwtService
{
    string GetJwtToken(string username, string role);
}
