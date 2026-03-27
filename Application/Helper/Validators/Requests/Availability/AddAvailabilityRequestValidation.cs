// Ignore Spelling: Validators

using Application.Requests.Availability; 
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.Availability
{
    public class AddAvailabilityRequestValidation : AbstractValidator<AddAvailabilityRequest>
    {
        public AddAvailabilityRequestValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ServicePrgIds)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.SERVICE_PRG);

            RuleForEach(x => x.ServicePrgIds)
                .GreaterThan(0).WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.SERVICE_PRG);
        }
    }
}
