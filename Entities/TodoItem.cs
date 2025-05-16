using System.ComponentModel.DataAnnotations;

namespace TodoAPI_Portfolio.Entities
{
    public class TodoItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

    }
}
