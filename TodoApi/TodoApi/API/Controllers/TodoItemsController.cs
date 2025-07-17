/*
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Models;
using TodoApi.Domain.Services.Interfaces;
using TodoApi.DTO.TodoItem;

namespace TodoApi.API.Controllers
{
    [Route("todos")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        
        [HttpGet]
        public async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetAllTodoItems([FromQuery] Priority? priority, [FromQuery] long? groupId, [FromQuery] bool? isCompleted)
        {
            if (priority != null)
            {
                return await _todoItemService.GetTodoItemByPriority(priority.Value);
            }
            else if (groupId != null)
            {
                return await _todoItemService.GetTodoItemByGroup(groupId.Value);
            }
            else if (isCompleted != null)
            {
                return await _todoItemService.GetTodoItemByCompletionStatus(isCompleted.Value);
            }
            return await _todoItemService.GetTodoItems();
        }
        
        
        [HttpGet("{id}")]
        public async Task<Results<Ok<TodoItemResponse>, BadRequest>> GetTodoItem(long id)
        {
            return await _todoItemService.GetTodoItemById(id);
        }
        
        [HttpPost]
        public async Task<Results<Ok<TodoItem>, BadRequest>> CreateTodoItem(CreateTodoItemRequest request)
        {
            return await _todoItemService.Create(request);
        }

        [HttpPut]
        public async Task<Results<Ok<TodoItem>, BadRequest, NotFound>> UpdateTodoItem(UpdateTodoItemRequest request)
        {
            return await _todoItemService.Update(request);
        }

        [HttpDelete("{id}")]
        public async Task<Results<Ok<DeleteTodoItemResponse>, BadRequest, NotFound>> DeleteTodoItem(DeleteRequest request)
        {
            return await _todoItemService.Delete(request);
        }
    }
}
*/
