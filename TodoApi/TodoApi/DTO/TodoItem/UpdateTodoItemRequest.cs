using TodoApi.Infrastructure.Models;

namespace TodoApi.DTO.TodoItem;

public class UpdateTodoItemRequest
{
    public string? Name { get; set; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
    public long GroupId { get; set; }
}