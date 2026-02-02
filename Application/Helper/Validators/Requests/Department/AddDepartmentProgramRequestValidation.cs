using System.Globalization;
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

            RuleFor(x => x.DateStart)
            .Must(date => string.IsNullOrEmpty(date) || DateOnly.TryParse(date, out _))
            .WithMessage(ValidationMessages.INVALID_DATE);

            RuleFor(x => x.DateEnd)
           .Must(date => string.IsNullOrEmpty(date) || DateOnly.TryParse(date, out _))
           .WithMessage(ValidationMessages.INVALID_DATE);

            RuleFor(x => x.DateStart)
            .Must((x, start) =>
            {
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(x.DateEnd))
                    return true; // ignore si l'une des dates est vide

                return DateOnly.TryParseExact(start, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dStart)
                    && DateOnly.TryParseExact(x.DateEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dEnd)
                    && dStart < dEnd; // start < end
            })
            .WithMessage(ValidationMessages.DATES_SUP);


            // Validation 1 : Si le programme est récurrent, Days ne doit pas être vide 
            RuleFor(x => x.Days)
            .NotEmpty()
            .When(x => x.IndRecurrent)
            .WithMessage(ValidationMessages.DAYS_REQUIRED); 

            RuleForEach(x => x.Days)
                .InclusiveBetween(0, 6)
                .WithMessage(ValidationMessages.VALID_DAYS)
                .When(x => x.IndRecurrent);

            //Les dates
            // Vérifie que la collection Dates n'est pas vide si le programme est récurrent
            RuleFor(x => x.Dates)
                .NotEmpty()
                .When(x => !x.IndRecurrent)
                .WithMessage(ValidationMessages.DATES_REQUIRED);

            // Vérifie que chaque date est valide si le programme est récurrent
            RuleForEach(x => x.Dates)
                .Must(date => DateOnly.TryParse(date, out _))
                .When(x => !x.IndRecurrent)
                .WithMessage(ValidationMessages.INVALID_DATE);

        }
    }
}
