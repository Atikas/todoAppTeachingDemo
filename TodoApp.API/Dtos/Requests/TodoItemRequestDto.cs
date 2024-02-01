using System.ComponentModel.DataAnnotations;
using TodoApp.API.Validators;

namespace TodoApp.API.Dtos
{
    public class TodoItemRequestDto
    {
        /// <summary>
        /// Type for the todo item, can be one of the following: 'Holiday', 'Work', 'Shopping', 'Other'
        /// </summary>
        [Required]
        [StringLength(100)]
        [TodoTypeValidator]
        public string? Type { get; set; }

        /// <summary>
        /// Title for the todo item
        /// </summary>
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Description for the todo item
        /// </summary>
        [StringLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// Date and time when the todo item is due
        /// </summary>
        [GreaterOrEqualToToday]
        public DateTime? Due { get; set; }

        /// <summary>
        /// Date and time when the todo item was completed
        /// </summary>
        [GreaterOrEqualToToday]
        public DateTime? CompletedAt { get; set; }

    }
}
