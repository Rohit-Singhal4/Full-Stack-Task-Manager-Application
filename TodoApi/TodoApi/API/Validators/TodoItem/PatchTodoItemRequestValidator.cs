using FluentValidation;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Models;

namespace TodoApi.API.Validators.TodoItem;

public class PatchTodoItemRequestValidator : AbstractValidator<PatchTodoItemRequest>
{
    public PatchTodoItemRequestValidator()
    {
        RuleFor(i => i.Name).MaximumLength(ModelValidationConstants.NameMaxLength);
        RuleFor(i => i.Description).MaximumLength(ModelValidationConstants.DescriptionMaxLength);
    }
}