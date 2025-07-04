using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp_API.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Name { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public DateOnly Deadline { get; set; }
        public int? TodoListId { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
