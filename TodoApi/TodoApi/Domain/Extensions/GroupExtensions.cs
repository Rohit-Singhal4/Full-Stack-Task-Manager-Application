using TodoApi.DTO;
using TodoApi.DTO.Group;
using TodoApi.Infrastructure.Models;

namespace TodoApi.Domain.Extensions;

internal static class GroupExtensions
{
    internal static GroupResponse ToGroupResponse(this Group group)
    {
        return new GroupResponse
        {
            Id = group.Id,
            Name = group.Name,
            IsDefault = group.IsDefault,
            ModifiedDate = group.ModifiedDate,
        };
    }
    
    internal static Group ToGroup(this CreateGroupRequest request)
    {
        return new Group
        {
            Name = request.Name,
            IsDefault = false,
            ModifiedDate = DateTime.UtcNow,
        };
    }
    
    internal static Group ToGroup(this UpdateGroupRequest request, Group existing)
    {
        existing.Name = request.Name;
        existing.ModifiedDate = DateTime.UtcNow;
        return existing;
    }
    
    internal static DeleteGroupResponse ToDeleteGroupResponse(this DeleteRequest request)
    {
        return new DeleteGroupResponse
        {
            Id = request.Id,
            DeletedAt = DateTime.UtcNow,
        };
    }
}