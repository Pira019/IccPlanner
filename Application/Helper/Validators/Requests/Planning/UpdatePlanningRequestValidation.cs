using Application.Requests.Planning;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.Planning
{
    public class UpdatePlanningRequestValidation : AbstractValidator<UpdatePlanningRequest>
    {
        public UpdatePlanningRequestValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage(string.Format(ValidationMessages.MAXLENGTH, "Comment", 500))
                .When(x => !string.IsNullOrEmpty(x.Comment));
        }
    }
}
