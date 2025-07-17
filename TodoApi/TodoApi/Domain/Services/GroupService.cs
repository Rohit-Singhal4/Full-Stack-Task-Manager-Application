using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Extensions;
using TodoApi.Domain.Services.Interfaces;
using TodoApi.DTO;
using TodoApi.DTO.Group;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Context;
using TodoApi.Infrastructure.Models;


namespace TodoApi.Domain.Services;

public class GroupService : IGroupService
{
    private readonly TodoContext _context;

    public GroupService (TodoContext context)
    {
        _context = context;
    }

    public async Task<Results<Ok<IEnumerable<GroupResponse>>, BadRequest>> GetGroups()
    {
        var groups = await _context.Groups.ToListAsync();
        return TypedResults.Ok(groups.Select(i => i.ToGroupResponse()));
    }

    public async Task<Results<Ok<GroupResponse>, BadRequest>> GetGroupById(long groupId)
    {
        var group = await _context.Groups.FindAsync(groupId);
        return TypedResults.Ok(group.ToGroupResponse());
    }
    
    public async Task<Results<Ok<IEnumerable<TodoItemResponse>>, BadRequest>> GetTodoItemsForGroup(long groupId)
    {
        IQueryable<TodoItem> todoItems = _context.TodoItems;
        todoItems = todoItems.Where(item => item.GroupId == groupId);
        var result = await todoItems.ToListAsync();
        return TypedResults.Ok(result.Select(i => i.ToTodoItemResponse()));
    }


    public async Task<Results<Ok<GroupResponse>, BadRequest>> Create(CreateGroupRequest request)
    {
        Group? existing = await _context.Groups.FirstOrDefaultAsync(g => g.Name == request.Name);
        if (existing is not null)
        {
            return TypedResults.BadRequest();
        }
        
        var group = request.ToGroup();
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();
        return TypedResults.Ok(group.ToGroupResponse());
    }

    public async Task<Results<Ok<GroupResponse>, BadRequest, NotFound>> Update(long id, UpdateGroupRequest request)
    {
        Group? existing = await _context.Groups.FindAsync(id);
        if (existing is null)
        {
            return TypedResults.NotFound();
        }
        var group = request.ToGroup(existing);
        _context.Groups.Update(group);
        await _context.SaveChangesAsync();
        
        return TypedResults.Ok(group.ToGroupResponse());
    }

    public async Task<Results<Ok<DeleteGroupResponse>, BadRequest, NotFound>> Delete(DeleteRequest request)
    {
        Group? existing = await _context.Groups.FindAsync(request.Id);
        if (existing is null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            _context.Groups.Remove(existing);
        }
        await _context.SaveChangesAsync();
        return TypedResults.Ok(request.ToDeleteGroupResponse());
    }
}


/*
IQueryable<Group> groups = _context.Groups.Include(i => i.Items);
//groups.Join(i => i)
if (groupId is not null)
{
    groups = groups.Where(item => item.Id == groupId);
}
        
if (priority is not null)
{
    groups = groups.Select(i => new Group()
    {
        Name = i.Name,
        Items = i.Items.Where(j => j.Priority == priority)
    });
}*/

/*
    public async Task<Results<Ok<IEnumerable<GroupResponse>>, BadRequest>> GetFilteredGroupsAndTodos(long? groupId,
        Priority? priority, bool? isCompleted)
    {
        IQueryable<Group> groups = _context.Groups.Include(i => i.Items);
        //groups.Join(i => i)
        if (groupId is not null)
        {
            groups = groups.Where(item => item.Id == groupId);
        }

        if (priority is not null)
        {
            groups = groups.Select(i => new Group()
            {
                Name = i.Name,
                IsDefault = i.IsDefault,
                Items = i.Items.Where(j => j.Priority == priority)
            });
        }
        if (isCompleted is not null)
        {
            groups = groups.Select(i => new Group()
            {
                Name = i.Name,
                IsDefault = i.IsDefault,
                Items = i.Items.Where(j => j.IsComplete == isCompleted)
            });
        }

        var result = await groups.ToListAsync();
        return TypedResults.Ok(result.Select(i => i.ToGroupResponse()));
    }
    */