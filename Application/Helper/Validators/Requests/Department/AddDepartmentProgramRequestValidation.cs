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

            RuleFor(x => x.TypePrg)
             .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.PROGRAM_TYPE)
             .IsEnumName(typeof(ProgramType), caseSensitive: false).WithMessage(ValidationMessages.INVALID_PRG_TYPE);

            // Validation 1 : Si le programme est récurrent, Days ne doit pas être vide
            RuleFor(x => x.Day)
                .NotEmpty()
                .When(x => x.TypePrg == ProgramType.Recurring.ToString()).WithMessage(ValidationMessages.DAYS_REQUIRED);

            // Validation 2 : Pour chaque jour, vérifier que ce n'est pas null si le programme est récurrent et Pour chaque jour, vérifier qu'il est valide
            RuleFor(x => x.Day)
               .NotEmpty()
               .When(x => x.TypePrg == ProgramType.Recurring.ToString()).WithMessage(ValidationMessages.CANNOT_CONTAIN_NULL).WithName(ValidationMessages.DAYS)
               .Must(day => Enum.TryParse(typeof(ValidDaysOfWeek), day, true, out _))
               .When(x => x.TypePrg == ProgramType.Recurring.ToString()).WithMessage(ValidationMessages.VALID_DAYS); 

            //Les dates
            RuleFor(x => x.Date)
                .NotEmpty()
                .When(date => date.TypePrg == ProgramType.Punctual.ToString()).WithMessage(ValidationMessages.DATES_REQUIRED);

            RuleForEach(x => x.Date)
                .Must(date => DateOnly.TryParse(date, out _))
                .When(date => date.TypePrg == ProgramType.Punctual.ToString()).WithMessage(ValidationMessages.INVALID_DATE); 
        } 
    }
}
