using System.ComponentModel.DataAnnotations;

namespace TodoApp.DAL.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }

    }
}
