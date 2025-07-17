using TodoApi.DTO;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Models;

namespace TodoApi.Domain.Extensions;

internal static class TodoItemExtensions
{
    internal static TodoItemResponse ToTodoItemResponse(this TodoItem todoItem)
    {
        return new TodoItemResponse
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            Priority = todoItem.Priority,
            DueDate = todoItem.DueDate,
            Description = todoItem.Description,
            ModifiedDate = todoItem.ModifiedDate,
            IsComplete = todoItem.IsComplete,
            GroupId = todoItem.GroupId,
        };
    }
    
    internal static TodoItem ToTodoItem(this CreateTodoItemRequest request, Group group)
    {
        return new TodoItem
        {
            Name = request.Name,
            Priority = request.Priority,
            DueDate = request.DueDate,
            Description = request.Description,
            ModifiedDate = DateTime.UtcNow,
            IsComplete = false,
            GroupId = group.Id,
        };
    }
    
    internal static TodoItem ToTodoItem(this UpdateTodoItemRequest request, TodoItem existing)
    {
        existing.Name = request.Name;
        existing.Priority = request.Priority;
        existing.DueDate = request.DueDate;
        existing.ModifiedDate = DateTime.UtcNow;
        existing.IsComplete = request.IsComplete;
        existing.Description = request.Description;
        existing.GroupId = request.GroupId;
        return existing;
    }
    
    internal static TodoItem ToTodoItem(this PatchTodoItemRequest request, TodoItem existing)
    {
        existing.Name = request.Name ?? existing.Name;
        existing.Priority = request.Priority ?? existing.Priority;
        existing.DueDate = request.DueDate ?? existing.DueDate;
        existing.ModifiedDate = DateTime.UtcNow;
        existing.Description = request.Description ?? existing.Description;
        existing.IsComplete = request.IsComplete ?? existing.IsComplete;
        existing.GroupId = request.GroupId ?? existing.GroupId;
        return existing;
    }
    
    internal static DeleteTodoItemResponse ToDeleteTodoItemResponse(this DeleteRequest request)
    {
        return new DeleteTodoItemResponse
        {
            Id = request.Id,
            DeletedAt = DateTime.UtcNow,
        };
    }
}