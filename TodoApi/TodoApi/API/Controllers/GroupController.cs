/*
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Domain.Services.Interfaces;
using TodoApi.DTO;
using TodoApi.DTO.Group;
using TodoApi.Infrastructure.Models;

namespace TodoApi.API.Controllers
{
    [Route("group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        
        [HttpGet]
        public async Task<Results<Ok<IEnumerable<GroupResponse>>, BadRequest>> GetAllGroups()
        {
            return await _groupService.GetGroups();
        }

        [HttpGet("{id}")]
        public async Task<Results<Ok<GroupResponse>, BadRequest>> GetGroup(long id)
        {
            return await _groupService.GetGroupById(id);
        }
        
        [HttpGet("todosOnly/{id}")]
        public async Task<Results<Ok<IEnumerable<TodoItem>>, BadRequest>> GetTodoItemsForGroup(long id)
        {
            return await _groupService.GetTodoItemsForGroup(id);
        }

        [HttpPost]
        public async Task<Results<Ok<Group>, BadRequest>> Create(CreateGroupRequest request)
        {
            return await _groupService.Create(request);
        }

        [HttpPut]
        public async Task<Results<Ok<Group>, BadRequest, NotFound>> Update(UpdateGroupRequest request)
        {
            return await _groupService.Update(request);
        }

        [HttpDelete]
        public async Task<Results<Ok<DeleteGroupResponse>, BadRequest, NotFound>> Delete(DeleteRequest request)
        {
            return await _groupService.Delete(request);
        }

    }
}
*/