using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp_API.Models;

namespace TodoApp_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly TodoDbContext _context;

        public TodoListController(TodoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoList>> GetTodoLists()
        {
            var todoLists = _context.TodoLists.Include(t => t.Items).ToList();
            return Ok(todoLists);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoList> GetTodoList(int id)
        {
            var todoList = _context.TodoLists.Find(id);
            if (todoList == null)
            {
                return NotFound();
            }
            return Ok(todoList);
        }

        [HttpPost]
        public ActionResult<TodoList> CreateTodoList([FromBody] TodoList todoList)
        {
            if (todoList == null || string.IsNullOrEmpty(todoList.Name))
            {
                return BadRequest("Invalid Todo List data.");
            }
            _context.TodoLists.Add(todoList);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTodoList), new { id = todoList.Id }, todoList);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodoList(int id, [FromBody] TodoList todoList)
        {
            if (todoList == null || todoList.Id != id || string.IsNullOrEmpty(todoList.Name))
            {
                return BadRequest("Invalid Todo List data.");
            }
            var existingTodoList = _context.TodoLists.Find(id);
            if (existingTodoList == null)
            {
                return NotFound();
            }
            existingTodoList.Name = todoList.Name;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodoList(int id)
        {
            var todoList = _context.TodoLists.Find(id);
            if (todoList == null)
            {
                return NotFound();
            }
            _context.TodoLists.Remove(todoList);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
