using System;
using Application.Requests.ServiceTab;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.ServieTab
{
    public class AddServiceTabValidation : AbstractValidator<AddServiceRequest>
    {
        public AddServiceTabValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.START_TIME)
                .Must(BeAValidTimeOnly).WithMessage(ValidationMessages.INVALID_ENTRY).WithName(ValidationMessages.START_TIME);

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.END_TIME)
                .Must(BeAValidTimeOnly).WithMessage(ValidationMessages.INVALID_ENTRY).WithName(ValidationMessages.END_TIME)
                .GreaterThan(x => x.StartTime).WithMessage(ValidationMessages.GREATER_THEN).WithName(ValidationMessages.END_TIME);

            RuleFor(x => x.MemberArrivalTime)
                .Must((model, arrivalTime) =>
                {
                    if (string.IsNullOrWhiteSpace(arrivalTime))
                        return true;

                    if (!BeAValidTimeOnly(arrivalTime))
                        return false;

                    return TimeOnly.Parse(arrivalTime) < TimeOnly.Parse(model.StartTime);

                }).WithMessage(ValidationMessages.MEMBER_ARIVAL_TIME);

            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DISPLAY_NAME)
                .MaximumLength(55).WithMessage(ValidationMessages.MAX_LENGTH).WithName(ValidationMessages.DISPLAY_NAME);

            RuleFor(x => x.Comment)
               .MaximumLength(55).WithMessage(ValidationMessages.MAX_LENGTH).WithName(ValidationMessages.COMMENT);
        }

        private bool BeAValidTimeOnly(string time)
        {
            try
            {
                // Vérification basique si l'heure est un TimeOnly valide
                return TimeOnly.Parse(time) != default(TimeOnly);
            }
            catch
            {
                return false; // Retourner faux si une exception se produit
            }
        }
    }
}
