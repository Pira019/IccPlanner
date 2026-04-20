using Application.Requests.Member;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.Member
{
    public class UpdateProfileRequestValidation : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .MaximumLength(55).WithMessage(string.Format(ValidationMessages.MAXLENGTH, "Name", 55))
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.LastName)
                .MaximumLength(55).WithMessage(string.Format(ValidationMessages.MAXLENGTH, "LastName", 55))
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.Sexe)
                .Must(s => s == "M" || s == "F").WithMessage(ValidationMessages.SEXE_IVALID)
                .When(x => !string.IsNullOrEmpty(x.Sexe));
        }
    }
}
