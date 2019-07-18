using FluentValidation;

namespace DocumentManagement.Application.Modules.Commands
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(x => x.Code)
                .Length(4, 128)
                .NotEmpty();

            RuleFor(x => x.Name)
                .Length(4, 128)
                .NotEmpty();
        }
    }
}
