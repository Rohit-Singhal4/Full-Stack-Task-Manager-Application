using Microsoft.AspNetCore.Http.HttpResults;
using TodoApi.DTO;
using TodoApi.DTO.Group;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Models;

namespace TodoApi.Domain.Services.Interfaces;

public interface IGroupService
{
    public Task<Results<Ok<IEnumerable<GroupResponse>>, BadRequest>> GetGroups();
    public Task<Results<Ok<GroupResponse>, BadRequest>> GetGroupById(long id);
    public Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemsForGroup(long id);

    //public Task<Results<Ok<IEnumerable<GroupResponse>>, BadRequest>> GetFilteredGroupsAndTodos(long? groupId, Priority? priority, bool? isCompleted);
    public Task<Results<Ok<GroupResponse>, BadRequest>> Create(CreateGroupRequest request);
    public Task<Results<Ok<GroupResponse>, BadRequest, NotFound>> Update(long id, UpdateGroupRequest request);
    public Task<Results<Ok<DeleteGroupResponse>, BadRequest, NotFound>> Delete(DeleteRequest request);
}