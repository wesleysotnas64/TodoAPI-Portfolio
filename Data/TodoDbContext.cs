using Microsoft.EntityFrameworkCore;
using TodoAPI_Portfolio.Entities;

namespace TodoAPI_Portfolio.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) {}

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
