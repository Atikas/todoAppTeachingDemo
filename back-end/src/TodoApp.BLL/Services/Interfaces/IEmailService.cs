namespace TodoApp.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string? to, string body);
    }
}
