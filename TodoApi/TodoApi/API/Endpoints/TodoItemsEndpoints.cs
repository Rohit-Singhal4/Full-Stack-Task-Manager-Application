using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TodoApi.API.Validators;
using TodoApi.API.Validators.TodoItem;
using TodoApi.Domain.Services.Interfaces;
using TodoApi.DTO;
using TodoApi.DTO.Group;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Models;

namespace TodoApi.API.Endpoints;

public static class TodoItemsEndpoints
{
    
    public static IEndpointRouteBuilder MapTodoItemEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder route = app.MapGroup("/todos").WithTags("TodoItems");
        route.MapGet("/", GetAllTodoItems).WithName(nameof(GetAllTodoItems));
        route.MapGet("/{id}", GetTodoItem).WithName(nameof(GetTodoItem));
        //route.MapGet("/filteredTodos", GetFilteredTodoItems).WithName(nameof(GetFilteredTodoItems));
        route.MapPost("/", CreateTodoItem).WithName(nameof(CreateTodoItem))
            .AddEndpointFilter<ValidationFilter<CreateTodoItemRequest>>();
        route.MapPut("/{id}", UpdateTodoItem).WithName(nameof(UpdateTodoItem))
            .AddEndpointFilter<ValidationFilter<UpdateTodoItemRequest>>();
        route.MapPatch("/{id}", PatchTodoItem).WithName(nameof(PatchTodoItem))
            .AddEndpointFilter<ValidationFilter<PatchTodoItemRequest>>();
        route.MapDelete("/", DeleteTodoItem).WithName(nameof(DeleteTodoItem))
            .AddEndpointFilter<ValidationFilter<DeleteRequest>>();
        return app;
    }
    
    public static async Task<Results<Ok<TodoItemResponse>, BadRequest>> GetTodoItem([FromServices] ITodoItemService service, [FromRoute] long id)
    {
        return await service.GetTodoItemById(id);
    }
    
    public static async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetAllTodoItems([FromServices] ITodoItemService service, [FromQuery] long? groupId, [FromQuery] bool? isCompleted, [FromQuery] SortOrder? order)
    {
        return await service.GetTodoItems(groupId, isCompleted, order);
    }

    public static async Task<Results<Ok<TodoItemResponse>, BadRequest>> CreateTodoItem([FromServices] ITodoItemService todoItemService, [FromBody] CreateTodoItemRequest request)
    {
        return await todoItemService.Create(request);
    }

    public static async Task<Results<Ok<TodoItemResponse>, BadRequest, NotFound>> UpdateTodoItem([FromServices] ITodoItemService service, [FromRoute] long id, [FromBody] UpdateTodoItemRequest request)
    {
        return await service.Update(id, request);
    }
    
    public static async Task<Results<Ok<TodoItemResponse>, BadRequest>> PatchTodoItem([FromServices] ITodoItemService service, [FromRoute] long id, [FromBody] PatchTodoItemRequest request)
    {
        
        /*if (priority != null)
        {
            return await service.PatchTodoItemByPriority(id, priority.Value);
        }
        if (dueDate != null)
        {
            return await service.PatchTodoItemByDueDate(id, dueDate.Value);
        }
        if (isCompleted != null)
        {
            return await service.PatchTodoItemByCompletionStatus(id, isCompleted.Value);
        }
        if (groupId != null)
        {
            return await service.PatchTodoItemByGroup(id, groupId.Value);
        }
        else
        {
            return TypedResults.BadRequest();
        }*/
        
        return await service.Patch(id, request);
    }

    public static async Task<Results<Ok<DeleteTodoItemResponse>, BadRequest, NotFound>> DeleteTodoItem([FromServices] ITodoItemService service, [FromBody] DeleteRequest request)
    {
        return await service.Delete(request);
    }
}

/*
 
 // groupId? add as nullable parameter for get requests
   public static async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetAllTodoItems([FromServices] ITodoItemService service, [FromQuery] Priority? priority, [FromQuery] DateTime? dueDate, [FromQuery] bool? isCompleted, [FromQuery] long? groupId, [FromQuery] SortOrder? order)
   {
       if (priority != null)
       {
           return await service.GetTodoItemByPriority(priority.Value);
       }
       else if (dueDate != null)
       {
           return await service.GetTodoItemByDueDate(dueDate.Value);
       }
       else if (isCompleted != null)
       {
           return await service.GetTodoItemByCompletionStatus(isCompleted.Value);
       }
       else if (groupId != null)
       {
           return await service.GetTodoItemByGroup(groupId.Value);
       }

       return await service.GetTodoItems();
   }
 
 */