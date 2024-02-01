using TodoApp.BLL.Services.Interfaces;

namespace TodoApp.BLL.Services
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(string? to, string body)
        {
            if (to != null)
            {
                // send email implementation
                return true;
            }

            return false;
        }
    }
}
