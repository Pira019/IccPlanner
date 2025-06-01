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

            RuleFor(x => x.ServicePrgId)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.SERVICE_PRG); 
        }
    }
}
