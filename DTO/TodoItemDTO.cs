namespace TodoAPI_Portfolio.DTO
{
    public class TodoItemDTO
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
