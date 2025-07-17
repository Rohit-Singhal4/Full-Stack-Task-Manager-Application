using FluentValidation;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Models;

namespace TodoApi.API.Validators.TodoItem;

public class CreateTodoItemRequestValidator : AbstractValidator<CreateTodoItemRequest>
{
    public CreateTodoItemRequestValidator()
    {
        RuleFor(i => i.Name).MaximumLength(ModelValidationConstants.NameMaxLength);
        RuleFor(i => i.Priority).NotEmpty();
        RuleFor(i => i.DueDate).NotEmpty();
    }
}