using System.ComponentModel.DataAnnotations;

namespace TodoApp.DAL.Entities
{
    public class Place
    {
        [Key]
        public long Id { get; set; }
        public string? Country { get; set; } = null!;
        public string? City { get; set; } = null!;

        public TodoItem TodoItem { get; set; } = null!;
    }
}
