using Application.Requests.Planning;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.Planning
{
    public class AssignMemberRequestValidation : AbstractValidator<AssignMemberRequest>
    {
        public AssignMemberRequestValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.AvailabilityId)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL)
                .GreaterThan(0).WithMessage(ValidationMessages.NOT_NULL);

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage(string.Format(ValidationMessages.MAXLENGTH, "Comment", 500))
                .When(x => !string.IsNullOrEmpty(x.Comment));
        }
    }
}
