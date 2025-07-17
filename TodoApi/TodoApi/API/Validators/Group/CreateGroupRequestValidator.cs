using FluentValidation;
using TodoApi.DTO.Group;
using TodoApi.Infrastructure.Models;

namespace TodoApi.API.Validators.Group;

public class CreateGroupRequestValidator : AbstractValidator<CreateGroupRequest>
{
    public CreateGroupRequestValidator()
    {
        RuleFor(i => i.Name).NotEmpty().MaximumLength(ModelValidationConstants.NameMaxLength).WithMessage("Group name");
    }
}