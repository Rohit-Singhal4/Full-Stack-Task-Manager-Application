using FluentValidation;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Models;

namespace TodoApi.API.Validators.TodoItem;

public class UpdateTodoItemRequestValidator : AbstractValidator<UpdateTodoItemRequest>
{
    public UpdateTodoItemRequestValidator()
    {
        RuleFor(i => i.Name).MaximumLength(ModelValidationConstants.NameMaxLength);
        RuleFor(i => i.Description).MaximumLength(ModelValidationConstants.DescriptionMaxLength);
        RuleFor(i => i.Priority).NotEmpty();
        RuleFor(i => i.GroupId).NotEmpty();
    }
}