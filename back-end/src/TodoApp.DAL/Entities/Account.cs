using System.ComponentModel.DataAnnotations;

namespace TodoApp.DAL.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Role { get; set; } = "user";

    }
}
