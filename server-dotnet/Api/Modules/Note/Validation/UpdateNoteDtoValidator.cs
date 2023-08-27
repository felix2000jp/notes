using Api.Modules.Note.Dto;
using FluentValidation;

namespace Api.Modules.Note.Validation;

public class UpdateNoteDtoValidator : AbstractValidator<UpdateNoteDto>
{
    public UpdateNoteDtoValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(request => request.Text)
            .NotEmpty()
            .MaximumLength(300);
    }
}