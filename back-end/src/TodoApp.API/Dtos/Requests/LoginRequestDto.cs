using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TodoApp.API.Validators;

namespace TodoApp.API.Dtos.Requests
{
    /// <summary>
    /// User account login request
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Username of the account
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? UserName { get; set; }
        /// <summary>
        /// Password of the account
        /// </summary>
        [PasswordValidator]
        public string? Password { get; set; }
    }
}
