using FluentValidation;
using MedApp.API.Resources;

namespace MedApp.API.Validators
{
    public class SavePatientResourceValidator : AbstractValidator<SavePatientResource>
    {
        public SavePatientResourceValidator()
        {
            const int maxLength = 50;

            RuleFor(a => a.FullName).NotEmpty().MaximumLength(maxLength);
        }
    }
}