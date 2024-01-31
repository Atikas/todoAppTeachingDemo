using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.DAL.Entities
{
    public class TodoItem
    {
        [Key]
        public long Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Due { get; set; }
        public DateTime? CompletedAt { get; set; }

        [ForeignKey(nameof(Account))]
        public Guid AccountId { get; set; }

        public Account Account { get; set; } = null!;
        public ICollection<Image> Images { get; set; } = null!;
    }
}
