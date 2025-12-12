using Application.Interfaces.Services;
using Application.Requests.Ministry;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests
{
    public class AddMinistryRequestValidation : AbstractValidator<AddMinistryRequest>
    {
        private readonly IMinistryService _ministryService;
        private readonly bool _isUpdate;
        public AddMinistryRequestValidation(IMinistryService ministryService, bool isUpdate = false) 
        {
            _ministryService = ministryService;
            _isUpdate = isUpdate;

            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(X => X.Name)
                .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.NAME)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.NAME)
                .MaximumLength(255).WithMessage(string.Format(ValidationMessages.MAXLENGTH, ValidationMessages.MINISTRY_NAME, 255));

            RuleFor(X => X.Description)
                .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION);

            When(x => !_isUpdate, () =>
            {
                RuleFor(X => X.Name)
                    .Must(IsMinistryExist)
                    .WithMessage(X => string.Format(ValidationMessages.MJ_MinistryNameExist, X.Name))
                    .WithName(ValidationMessages.MINISTRY_NAME); 
            });

            When(x => _isUpdate, () =>
            {
                RuleFor(X => X.Id)
                    .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.Id)
                    .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.Id);
            });


           
        }

        private bool IsMinistryExist(String ministryName)
        {
            return !_ministryService.IsNameMinistryExists(ministryName).Result;
        }
    }
}
