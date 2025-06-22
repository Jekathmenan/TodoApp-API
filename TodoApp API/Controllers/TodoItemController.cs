using Microsoft.AspNetCore.Mvc;
using TodoApp_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly TodoDbContext _context;
        public TodoItemController(TodoDbContext context)
        {
            _context = context;
        }

        // GET: api/<TodoItemController>
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            var todoItems = _context.TodoItems.ToList();
            return Ok(todoItems);
        }

        // GET api/<TodoItemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TodoItemController>
        [HttpPost]
        public ActionResult<TodoItem> Post([FromBody] TodoItem todoItem)
        {
            if (todoItem == null || string.IsNullOrEmpty(todoItem.Name))
            {
                return BadRequest("Invalid Todo Item data.");
            }
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
        }

        // PUT api/<TodoItemController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TodoItem todoItem)
        {
            if (todoItem == null || todoItem.Id != id || string.IsNullOrEmpty(todoItem.Name))
            {
                return BadRequest("Invalid Todo Item data.");
            }
            var existingTodoItem = _context.TodoItems.Find(id);
            if (existingTodoItem == null)
            {
                return NotFound();
            }
            existingTodoItem.Name = todoItem.Name;
            existingTodoItem.Deadline = todoItem.Deadline;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<TodoItemController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todoItem = _context.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
