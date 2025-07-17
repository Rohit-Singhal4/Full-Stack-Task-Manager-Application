using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTO;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Models;


namespace TodoApi.Domain.Services.Interfaces;

public interface ITodoItemService
{
    public Task<Results<Ok<TodoItemResponse>, BadRequest>> GetTodoItemById(long id);
    public Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItems(long? groupId, bool? isCompleted, SortOrder? order);
    public Task<Results<Ok<TodoItemResponse>, BadRequest>> Create(CreateTodoItemRequest request);
    public Task<Results<Ok<TodoItemResponse>, BadRequest, NotFound>> Update(long id, UpdateTodoItemRequest request);
    public Task<Results<Ok<TodoItemResponse>, BadRequest>> Patch(long id, PatchTodoItemRequest request);
    public Task<Results<Ok<DeleteTodoItemResponse>, BadRequest, NotFound>> Delete(DeleteRequest request);
    
    //public Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItems();
    //public Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemByGroup(long groupId);
    //public Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemByPriority(Priority priority);
    //public Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemByCompletionStatus(bool isCompleted);
    //public Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemByDueDate(DateTime dueDate);
    //public Task<Results<Ok<TodoItemResponse>, BadRequest>> PatchTodoItemByPriority(long id, Priority priority);
    //public Task<Results<Ok<TodoItemResponse>, BadRequest>> PatchTodoItemByDueDate(long id, DateTime dueDate);
    //public Task<Results<Ok<TodoItemResponse>, BadRequest>> PatchTodoItemByCompletionStatus(long id, bool isCompleted);
    //public Task<Results<Ok<TodoItemResponse>, BadRequest>> PatchTodoItemByGroup(long id, long groupId);
}