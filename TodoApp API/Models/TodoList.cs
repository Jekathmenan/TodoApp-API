using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp_API.Models
{
    public class TodoList
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = string.Empty;
        public List<TodoItem> Items { get; set; } = new List<TodoItem>();
    }
}
