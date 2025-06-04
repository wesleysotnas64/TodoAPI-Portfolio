using Microsoft.AspNetCore.Mvc;
using TodoAPI_Portfolio.Entities;
using TodoAPI_Portfolio.Services;
using TodoAPI_Portfolio.DTO;
using Microsoft.AspNetCore.SignalR;

namespace TodoAPI_Portfolio.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/todo
        [HttpGet("todo")]
        public IActionResult GetAll()
        {
            var items = _todoService.GetAll();
            return Ok(items);
        }

        // GET: api/todo/{id}
        [HttpGet("todo/{id}")]
        public IActionResult GetByIde(Guid id)
        {
            var item = _todoService.GetById(id);
            if(item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
        }

        // POST: api/todo
        [HttpPost("todo")]
        public async Task<IActionResult> Save([FromBody] TodoItemDTO item)
        {
            TodoItem todoItem = new()
            {
                Id = Guid.NewGuid(),
                Title = item.Title,
                Description = item.Description,
                IsCompleted = false
            };

            _todoService.Save(todoItem);

            return Ok();
        }

        // PUT: api/todo
        [HttpPut("todo")]
        public async Task<IActionResult> Update([FromBody] TodoItem item)
        {
            bool updated = _todoService.Update(item);
            if (updated)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/todo/{id}
        [HttpDelete("todo/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = _todoService.Delete(id);
            if(deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
