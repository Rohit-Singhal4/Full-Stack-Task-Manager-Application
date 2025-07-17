using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApi.API.Validators;
using TodoApi.API.Validators.Group;
using TodoApi.Domain.Services.Interfaces;
using TodoApi.DTO;
using TodoApi.DTO.Group;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Models;

namespace TodoApi.API.Endpoints;

public static class GroupEndpoints
{
    
    public static IEndpointRouteBuilder MapGroupEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder route = app.MapGroup("/groups").WithTags("Groups");
        route.MapGet("/", GetAllGroups).WithName(nameof(GetAllGroups));
        route.MapGet("/{id}", GetGroup).WithName(nameof(GetGroup));
        //route.MapGet("/todosOnly/{id}", GetTodoItemsForGroup).WithName(nameof(GetTodoItemsForGroup));
        //route.MapGet("/groupsAndTodos", GetFilteredGroupsAndTodos).WithName(nameof(GetFilteredGroupsAndTodos));
        route.MapPost("/", CreateGroup).WithName(nameof(CreateGroup))
            .AddEndpointFilter<ValidationFilter<CreateGroupRequest>>();
        route.MapPut("/{id}", UpdateGroup).WithName(nameof(UpdateGroup))
            .AddEndpointFilter<ValidationFilter<UpdateGroupRequest>>();
        route.MapDelete("/", DeleteGroup).WithName(nameof(DeleteGroup))
            .AddEndpointFilter<ValidationFilter<DeleteRequest>>();
        return app;
    }
    
    public static async Task<Results<Ok<IEnumerable<GroupResponse>>, BadRequest>> GetAllGroups([FromServices] IGroupService service)
    {
        return await service.GetGroups();
    }
    
    public static async Task<Results<Ok<GroupResponse>, BadRequest>> GetGroup([FromServices] IGroupService service, [FromQuery] long id)
    {
        return await service.GetGroupById(id);
    }
    
    public static async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemsForGroup([FromServices] IGroupService service, [FromQuery] long id)
    {
        return await service.GetTodoItemsForGroup(id);
    }

    /*
    public static async Task<Results<Ok<IEnumerable<GroupResponse>>, BadRequest>> GetFilteredGroupsAndTodos([FromServices] IGroupService service, [FromQuery] long? groupId, [FromQuery] Priority? priority, [FromQuery] bool? isCompleted)
    {
        return await service.GetFilteredGroupsAndTodos(groupId, priority, isCompleted);
    }
    */

    public static async Task<Results<Ok<GroupResponse>, BadRequest>> CreateGroup([FromServices] IGroupService service, [FromBody] CreateGroupRequest request)
    {
        return await service.Create(request);
    }
    
    public static async Task<Results<Ok<GroupResponse>, BadRequest, NotFound>> UpdateGroup([FromServices] IGroupService service, [FromRoute] long id, [FromBody] UpdateGroupRequest request)
    {
        return await service.Update(id, request);
    }
    
    public static async Task<Results<Ok<DeleteGroupResponse>, BadRequest, NotFound>> DeleteGroup([FromServices] IGroupService service, [FromBody] DeleteRequest request)
    {
        return await service.Delete(request);
    }
    
}

