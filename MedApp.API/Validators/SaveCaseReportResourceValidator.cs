using FluentValidation;
using MedApp.API.Resources;

namespace MedApp.API.Validators
{
    public class SaveCaseReportResourceValidator : AbstractValidator<SaveCaseReportResource>
    {
        public SaveCaseReportResourceValidator()
        {
            const int maxLength = 50;
            const string errorMsg = "'Patient Id' must be greater than 0.";

            RuleFor(m => m.Name).NotEmpty().MaximumLength(maxLength);

            RuleFor(m => m.PatientId).NotEmpty().WithMessage(errorMsg);
        }
    }
}