using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Models;

namespace TodoApi.Infrastructure.Models;

public class TodoItem
{
    public long Id { get; set; } // unique key in a relational database
    public string Name { get; set; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; } = null!;
}