using System.ComponentModel.DataAnnotations;
using TodoApp.API.Validators;

namespace TodoApp.API.Dtos.Requests
{
    public record AccountRequestDto
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

        /// <summary>
        /// Email of the account
        /// </summary>
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Role of the account
        /// </summary>
        [RoleValidator]
        public string? Role { get; set; }
    }



}
