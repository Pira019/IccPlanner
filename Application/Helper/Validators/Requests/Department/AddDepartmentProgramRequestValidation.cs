using Application.Requests.Department;
using FluentValidation;
using Shared.Enums;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.Department
{
    public class AddDepartmentProgramRequestValidation : AbstractValidator<AddDepartmentProgramRequest>
    {
        public AddDepartmentProgramRequestValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.DepartmentIds)
               .NotEmpty().WithMessage(ValidationMessages.MUST_CHOOSE).WithName(ValidationMessages.DEPARTMENT_.ToLower());

            RuleForEach(x => x.DepartmentIds)
               .NotNull().WithMessage(ValidationMessages.CANNOT_CONTAIN_NULL).WithName(ValidationMessages.DEPARTMENT);

            RuleFor(x => x.ProgramId)
              .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.PROGRAM_NAME);

            /*RuleFor(x => x.IndRecurent)
             .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.PROGRAM_TYPE);*/

            // Validation 1 : Si le programme est récurrent, Days ne doit pas être vide
            RuleFor(x => x.Days)
                .NotEmpty()
                .When(x => x.IndRecurent).WithMessage(ValidationMessages.DAYS_REQUIRED);

            // Validation 2 : Pour chaque jour, vérifier que ce n'est pas null si le programme est récurrent et Pour chaque jour, vérifier qu'il est valide
            RuleForEach(x => x.Days)
             .NotEmpty()
             .When(x => x.IndRecurent)
             .WithMessage(ValidationMessages.CANNOT_CONTAIN_NULL)
             .Must(day => int.TryParse(day, out int dayInt) && dayInt >= 1 && dayInt <= 7)
             .When(x => x.IndRecurent)
             .WithMessage(ValidationMessages.VALID_DAYS);

            //Les dates            
            RuleForEach(x => x.Dates)
            .Must(date => DateOnly.TryParse(date, out _))
            .When(x => !x.IndRecurent)
            .WithMessage(ValidationMessages.INVALID_DATE)
            .Must(date =>
            {
                if (DateOnly.TryParse(date, out var parsedDate))
                {
                    return parsedDate >= DateOnly.FromDateTime(DateTime.Today);
                }
                return true; // Si invalide, déjà géré par la règle précédente
            })
            .When(x => !x.IndRecurent)
            .WithMessage(ValidationMessages.DATE_INF);
        }
    }
}
