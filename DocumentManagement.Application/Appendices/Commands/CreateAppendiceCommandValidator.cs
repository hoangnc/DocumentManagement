using FluentValidation;

namespace DocumentManagement.Application.Appendices.Commands
{
    public class CreateAppendiceCommandValidator : AbstractValidator<CreateAppendiceCommand>
    {
        public CreateAppendiceCommandValidator()
        {
            RuleFor(x => x.Code)
                .Length(4, 128)
                .NotEmpty();

            RuleFor(x => x.CompanyCode).MaximumLength(64);
            RuleFor(x => x.CompanyName).MaximumLength(200);
            RuleFor(x => x.DepartmentCode).MaximumLength(64);
            RuleFor(x => x.DepartmentName).MaximumLength(200);
            RuleFor(x => x.Module).MaximumLength(128);
            RuleFor(x => x.DocumentType).MaximumLength(128);
            RuleFor(x => x.Name).MaximumLength(512);
            RuleFor(x => x.FileName).MaximumLength(128);
            RuleFor(x => x.AppendiceNumber).MaximumLength(128);
            RuleFor(x => x.ReviewNumber).MaximumLength(128);
            RuleFor(x => x.Drafter).MaximumLength(128);
            RuleFor(x => x.Auditor).MaximumLength(128);
            RuleFor(x => x.Approver).MaximumLength(128);
            RuleFor(x => x.ScopeOfApplication).MaximumLength(512);
            RuleFor(x => x.ScopeOfDeloyment).MaximumLength(128);
            RuleFor(x => x.ReplaceOf).MaximumLength(128);
            RuleFor(x => x.RelateToDocuments).MaximumLength(128);
            RuleFor(x => x.LinkFile).MaximumLength(1024);
        }
    }
}
