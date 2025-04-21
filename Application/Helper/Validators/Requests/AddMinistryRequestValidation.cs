using Application.Interfaces.Services;
using Application.Requests.Ministry;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests
{
    public class AddMinistryRequestValidation : AbstractValidator<AddMinistryRequest>
    {
        private readonly IMinistryService _ministryService;
       public AddMinistryRequestValidation(IMinistryService ministryService) 
        {
            _ministryService = ministryService;

            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(X => X.Name)
                .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.NAME)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.NAME)
                .MaximumLength(255).WithMessage(string.Format(ValidationMessages.MAXLENGTH, ValidationMessages.MINISTRY_NAME, 255))
                .Must(IsMinistryExist).WithMessage(ValidationMessages.NAME_EXISTS).WithName(ValidationMessages.MINISTRY_NAME);

            RuleFor(X => X.Description)
               .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION)
               .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION);
        }

        private bool IsMinistryExist(String ministryName)
        {
            return !_ministryService.IsNameMinistryExists(ministryName).Result;
        }
    }
}
