namespace TodoApi.DTO.TodoItem;

public class DeleteTodoItemResponse
{
    public long Id { get; set; }
    public DateTime? DeletedAt { get; set; }
}