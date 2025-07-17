using System.Data;
using FluentValidation;
using TodoApi.DTO.Group;
using TodoApi.Infrastructure.Models;

namespace TodoApi.API.Validators.Group;

public class UpdateGroupRequestValidator : AbstractValidator<UpdateGroupRequest>
{
    public UpdateGroupRequestValidator()
    {
        RuleFor(i => i.Name).NotEmpty().MaximumLength(ModelValidationConstants.NameMaxLength);
    }
}