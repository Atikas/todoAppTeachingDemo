using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.DAL.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ImageBytes { get; set; }

        [ForeignKey(nameof(TodoItem))]
        public long TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; } = null!;
    }
}
