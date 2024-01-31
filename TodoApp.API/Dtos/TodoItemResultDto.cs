namespace TodoApp.API.Dtos
{
    public class TodoItemResultDto
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Due { get; set; }
        public DateTime? CompletedAt { get; set; }

    }
}
