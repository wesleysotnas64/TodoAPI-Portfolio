using TodoAPI_Portfolio.Data;
using TodoAPI_Portfolio.Entities;

namespace TodoAPI_Portfolio.Services
{
    public class TodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public List<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        public TodoItem? GetById(Guid id)
        {
            return _context.TodoItems.Find(id);
        }

        public void Save(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public bool Update(TodoItem updatedItem)
        {
            var existingItem = _context.TodoItems.Find(updatedItem.Id);
            if (existingItem == null)
                return false;

            existingItem.Title = updatedItem.Title;
            existingItem.Description = updatedItem.Description;
            existingItem.IsCompleted = updatedItem.IsCompleted;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
                return false;

            _context.TodoItems.Remove(item);
            _context.SaveChanges();
            return true;
        }
    }
}
