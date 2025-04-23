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
                .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DEPARTMENT_NAME)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DEPARTMENT_NAME)
                .MaximumLength(255).WithMessage(ValidationMessages.MAX_LENGTH).WithMessage(ValidationMessages.DEPARTMENT_NAME)
                .Must(IsDepartmentNameExists).WithMessage(ValidationMessages.DEPARTMENT_EXIST);

            RuleFor(x => x.MinistryId)
                .NotEmpty().WithMessage(ValidationMessages.INVALID_ENTRY).WithName(ValidationMessages.MINISTRY)
                .Must(IsMinistryExistsById).WithMessage(ValidationMessages.INVALID_ENTRY).WithName(ValidationMessages.MINISTRY);

            RuleFor(x => x.ShortName)
               .MaximumLength(55).WithMessage(ValidationMessages.MAX_LENGTH).WithMessage(ValidationMessages.SHORT_NAME);

            RuleFor(X => X.Description)
              .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION)
              .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION);
        }

        /// <summary>
        /// Verifier si le nom département existe deja 
        /// </summary>
        /// <param name="departmentName">
        /// Nom du département
        /// </param>
        /// <returns></returns>
        private bool IsDepartmentNameExists(string departmentName)
        {
            return !_departmentService.IsNameExists(departmentName).Result;
        }

        /// <summary>
        /// Verifier si l'Id département n'existe pas
        /// </summary>
        /// <param name="ministryId">
        /// L'Id du département
        /// </param>
        /// <returns></returns>
        private bool IsMinistryExistsById(int ministryId)
        {
            return _ministryService.IsMinistryExistsById(ministryId).Result;
        }
    }
}
