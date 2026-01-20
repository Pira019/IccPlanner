using Application.Interfaces.Services;
using Application.Requests.Department;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.Ministry
{
    /// <summary>
    /// Validation lors de l'ajout d'un département 
    /// </summary>
    public class AddDepartmentRequestValidation : AbstractValidator<AddDepartmentRequest>
    {
        private readonly IMinistryService _ministryService;
        private readonly IDepartmentService _departmentService;

        public AddDepartmentRequestValidation(IMinistryService ministryService, IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            _ministryService = ministryService;

            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DEPARTMENT_NAME)
                .MaximumLength(255).WithMessage(ValidationMessages.MAX_LENGTH).WithMessage(ValidationMessages.DEPARTMENT_NAME); 

            RuleFor(x => x.ShortName)
               .MaximumLength(55).WithMessage(ValidationMessages.MAX_LENGTH).WithMessage(ValidationMessages.SHORT_NAME);

            RuleFor(X => X.Description) 
              .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION);
            
            RuleFor(X => X.StartDate)
              .Must(date => date == null || date.Value <= DateOnly.FromDateTime(DateTime.Today)).WithMessage(ValidationMessages.VAILID_DATE).WithName(ValidationMessages.DATE);
        } 
    }
}
