using Application.Requests.Poste;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.Poste
{
    public class AddPosteRequestValidation : AbstractValidator<AddPosteRequest>
    {
        public AddPosteRequestValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.NAME)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.NAME)
                .MaximumLength(55).WithMessage(string.Format(ValidationMessages.MAXLENGTH, ValidationMessages.NAME, 55))
                .Must(name => char.IsUpper(name[0]) && name[1..] == name[1..].ToLower())
                .WithMessage(string.Format(ValidationMessages.CAPITALIZE_FORMAT, ValidationMessages.NAME));

            RuleFor(x => x.Description)
                .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION);

            RuleFor(x => x.ShortName)
                .MaximumLength(15).WithMessage(string.Format(ValidationMessages.MAXLENGTH, "ShortName", 15))
                .When(x => !string.IsNullOrEmpty(x.ShortName));
        }
    }
}
