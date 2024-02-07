using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.DAL.Entities
{
    public class TodoItem
    {
        [Key]
        public long Id { get; set; }
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Place? Place { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Due { get; set; }
        public DateTime? CompletedAt { get; set; }

        [ForeignKey(nameof(Account))]
        public Guid AccountId { get; set; }

        public Account Account { get; set; } = null!;
        public ICollection<Image> Images { get; set; } = null!;
    }
}
