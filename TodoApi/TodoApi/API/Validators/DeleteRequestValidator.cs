using FluentValidation;
using TodoApi.DTO;

namespace TodoApi.API.Validators;

public class DeleteRequestValidator : AbstractValidator<DeleteRequest>
{
    public DeleteRequestValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
    }
}