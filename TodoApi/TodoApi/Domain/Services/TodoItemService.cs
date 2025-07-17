using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Context;
using TodoApi.Infrastructure.Models;
using TodoApi.Domain.Services.Interfaces;
using TodoApi.DTO.TodoItem;
using TodoApi.Domain.Extensions;
using TodoApi.DTO;

namespace TodoApi.Domain.Services;

public class TodoItemService : ITodoItemService
{
    private readonly TodoContext _context;

    public TodoItemService (TodoContext context)
    {
        _context = context;
    }
    
    public async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItems(long? groupId, bool? isCompleted, SortOrder? order)
    {
        IQueryable<TodoItem> todoItems = _context.TodoItems;
        if (groupId is not null)
        {
            todoItems = todoItems.Where(item => item.GroupId == groupId);
        }
        //if (priority is not null) {
        //    todoItems = todoItems.Where(item => item.Priority == priority);
        //}
        if (isCompleted is not null)
        {
            todoItems = todoItems.Where(item => item.IsComplete == isCompleted);
        }
        todoItems = order switch
        {
            SortOrder.DueDateAsc => todoItems.OrderBy(i => i.DueDate),
            SortOrder.DueDateDesc => todoItems.OrderByDescending(i => i.DueDate),
            SortOrder.PriorityAsc => todoItems.OrderBy(i => i.Priority),
            SortOrder.PriorityDesc => todoItems.OrderByDescending(i => i.Priority),
            _=> todoItems.OrderBy(i => i.Priority)
        };

        var result = await todoItems.ToListAsync();
        return TypedResults.Ok(result.Select(i => i.ToTodoItemResponse()));
    }
    
    public async Task<Results<Ok<TodoItemResponse>, BadRequest>> GetTodoItemById(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem is null)
        {
            return TypedResults.BadRequest();
        }

        return TypedResults.Ok(todoItem.ToTodoItemResponse());
    }
    
    public async Task<Results<Ok<TodoItemResponse>, BadRequest>> Create(CreateTodoItemRequest request)
    {
        var existing = request.GroupId is null
            ? _context.Groups.FirstOrDefault(i => i.IsDefault)
            : _context.Groups.FirstOrDefault(i => i.Id == request.GroupId);

        if (existing is null && request.GroupId is null)
        {
            existing = new Group()
            {
                IsDefault = true,
                Name = "Default",
                ModifiedDate = DateTime.UtcNow
            };
            await _context.Groups.AddAsync(existing);
            await _context.SaveChangesAsync();
        }
        if (existing is null)
        {
            return TypedResults.BadRequest();
        }
        
        var todoItem = request.ToTodoItem(existing);
        await _context.TodoItems.AddAsync(todoItem);
        await _context.SaveChangesAsync();
        return TypedResults.Ok(todoItem.ToTodoItemResponse());
    }

    public async Task<Results<Ok<TodoItemResponse>, BadRequest, NotFound>> Update(long id, UpdateTodoItemRequest request)
    {
        TodoItem? existing = await _context.TodoItems.FindAsync(id); 
        if (existing is null)
        {
            return TypedResults.NotFound();
        }
        
        var todoItem = request.ToTodoItem(existing);
        _context.TodoItems.Update(todoItem);
        await _context.SaveChangesAsync();
        
        return TypedResults.Ok(todoItem.ToTodoItemResponse());
    }

    public async Task<Results<Ok<TodoItemResponse>, BadRequest>> Patch(long id, PatchTodoItemRequest request)
    {
        TodoItem? existing = await _context.TodoItems.FindAsync(id);
        if (existing is null)
        {
            return TypedResults.BadRequest();
        }

        var todoItem = request.ToTodoItem(existing);
        _context.TodoItems.Update(todoItem);
        await _context.SaveChangesAsync();

        return TypedResults.Ok(todoItem.ToTodoItemResponse());
    }

    public async Task<Results<Ok<DeleteTodoItemResponse>, BadRequest, NotFound>> Delete(DeleteRequest request)
    {
        if (!TodoItemExists(request.Id))
        {
            return TypedResults.BadRequest();
        }
        var todoItem = await _context.TodoItems.FindAsync(request.Id);
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return TypedResults.Ok<DeleteTodoItemResponse>(request.ToDeleteTodoItemResponse());
    }
    
    private bool TodoItemExists(long id)
    {
        return _context.TodoItems.Any(item => item.Id == id);
    }
}
    
    /*
     * PATCH ENDPOINT FUNCTIONS
     *
     
     public async Task<Results<Ok<TodoItemResponse>, BadRequest>> PatchTodoItemByPriority(long id, Priority priority)
       {
           TodoItem? existing = await _context.TodoItems.FindAsync(id);
           if (existing is null)
           {
               return TypedResults.BadRequest();
           }

           existing.Priority = priority;
           _context.TodoItems.Update(existing);
           await _context.SaveChangesAsync();

           return TypedResults.Ok(existing.ToTodoItemResponse());
       }

       public async Task<Results<Ok<TodoItemResponse>, BadRequest>> PatchTodoItemByDueDate(long id, DateTime dueDate)
       {
           TodoItem? existing = await _context.TodoItems.FindAsync(id);
           if (existing is null)
           {
               return TypedResults.BadRequest();
           }

           existing.DueDate = dueDate;
           _context.TodoItems.Update(existing);
           await _context.SaveChangesAsync();

           return TypedResults.Ok(existing.ToTodoItemResponse());
       }

       public async Task<Results<Ok<TodoItemResponse>, BadRequest>> PatchTodoItemByCompletionStatus(long id, bool isCompleted)
       {
           TodoItem? existing = await _context.TodoItems.FindAsync(id);
           if (existing is null)
           {
               return TypedResults.BadRequest();
           }

           existing.IsComplete = isCompleted;
           _context.TodoItems.Update(existing);
           await _context.SaveChangesAsync();

           return TypedResults.Ok(existing.ToTodoItemResponse());
       }

       public async Task<Results<Ok<TodoItemResponse>, BadRequest>> PatchTodoItemByGroup(long id, long groupId)
       {
           TodoItem? existing = await _context.TodoItems.FindAsync(id);
           if (existing is null)
           {
               return TypedResults.BadRequest();
           }

           var group = await _context.Groups.FindAsync(groupId);
           if (group is null)
           {
               return TypedResults.BadRequest();
           }

           existing.GroupId = groupId;
           _context.TodoItems.Update(existing);
           await _context.SaveChangesAsync();

           return TypedResults.Ok(existing.ToTodoItemResponse());
       }
     
     */
     
     
     
     /*
         public async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItems()
        {
            var todoItemsList = await _context.TodoItems.ToListAsync();
            return TypedResults.Ok(todoItemsList.Select(i => i.ToTodoItemResponse()));
        }
         
         public async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemByPriority(Priority priority)
        {
            var todoItems = await _context.TodoItems.Where(item => item.Priority == priority).ToListAsync();
            return TypedResults.Ok<IEnumerable<TodoItemResponse>>(todoItems.Select(i => i.ToTodoItemResponse()));
        }
        
        
        public async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemByDueDate(DateTime dueDate)
        {
            var todoItems = await _context.TodoItems.Where(item => item.DueDate == dueDate).ToListAsync();
            return TypedResults.Ok(todoItems.Select(i => i.ToTodoItemResponse()));
        }
        
        
        public async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemByCompletionStatus(bool isCompleted)
        {
            var todoItems = await _context.TodoItems.Where(item => item.IsComplete == isCompleted).ToListAsync();
            return TypedResults.Ok<IEnumerable<TodoItemResponse>>(todoItems.Select(i => i.ToTodoItemResponse()));
        }
        
        public async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemByGroup(long groupId)
        {   
            var todoItems = await _context.TodoItems.Where(item => item.GroupId == groupId).ToListAsync();
            return TypedResults.Ok<IEnumerable<TodoItemResponse>>(todoItems.Select(i => i.ToTodoItemResponse()));
        }
    */