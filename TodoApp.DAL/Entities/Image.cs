using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.DAL.Entities
{
    public class Image
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[] ImageBytes { get; set; } = null!;

        [ForeignKey(nameof(TodoItem))]
        public long TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; } = null!;
    }
}
