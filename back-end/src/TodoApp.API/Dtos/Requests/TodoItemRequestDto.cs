using System.ComponentModel.DataAnnotations;
using TodoApp.API.Validators;

namespace TodoApp.API.Dtos
{
    public class TodoItemRequestDto
    {
        /// <summary>
        /// Type for the todoitem, can be one of the following: 'Holiday', 'Work', 'Shopping', 'Other'
        /// </summary>
        [Required]
        [StringLength(100)]
        [TodoTypeValidator]
        public string? Type { get; set; }

        /// <summary>
        /// Title for the todoitem
        /// </summary>
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Description for the todoitem
        /// </summary>
        [StringLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// Place for the todoitem to be done
        /// </summary>
        [StringLength(100)]
        public string? Place { get; set; }

        /// <summary>
        /// Date and time when the todoitem is due
        /// </summary>
        [GreaterOrEqualToToday]
        public DateTime? Due { get; set; }

        /// <summary>
        /// Date and time when the todoitem was completed
        /// </summary>
        [LessOrEqualToToday]
        public DateTime? CompletedAt { get; set; }

    }
}
