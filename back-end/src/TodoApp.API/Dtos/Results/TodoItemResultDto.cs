namespace TodoApp.API.Dtos.Results
{
    // Remove null values when serializing to json

    public class TodoItemResultDto
    {
        public long Id { get; set; }
        /// <summary>
        /// Type for the todoitem, can be one of the following: 'Holiday', 'Work', 'Shopping', 'Other'
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Title for the todoitem
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description for the todoitem
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Place for the todoitem to be done
        /// </summary>
        public string? Place { get; set; }

        /// <summary>
        /// Date and time when the todoitem was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time when the todoitem is due
        /// </summary>
        public DateTime? Due { get; set; }

        /// <summary>
        /// Date and time when the todoitem was completed
        /// </summary>
        public DateTime? CompletedAt { get; set; }

    }
}
