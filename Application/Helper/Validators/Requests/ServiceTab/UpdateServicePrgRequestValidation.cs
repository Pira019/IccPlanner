using Application.Requests.ServiceTab;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.ServiceTab
{
    public class UpdateServicePrgRequestValidation : AbstractValidator<UpdateServicePrgRequest>
    {
        public UpdateServicePrgRequestValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            // StartTime < EndTime
            RuleFor(x => x.EndTime)
                .Must((req, endTime) =>
                {
                    if (string.IsNullOrWhiteSpace(req.StartTime) || string.IsNullOrWhiteSpace(endTime))
                    {
                        return true;
                    }
                    return TimeOnly.Parse(endTime) > TimeOnly.Parse(req.StartTime);
                })
                .WithMessage(ValidationMessages.GREATER_THEN)
                .When(x => !string.IsNullOrWhiteSpace(x.StartTime) && !string.IsNullOrWhiteSpace(x.EndTime));

            // ArrivalTime <= StartTime
            RuleFor(x => x.ArrivalTimeOfMember)
                .Must((req, arrival) =>
                {
                    if (string.IsNullOrWhiteSpace(arrival) || string.IsNullOrWhiteSpace(req.StartTime))
                    {
                        return true;
                    }
                    return TimeOnly.Parse(arrival) <= TimeOnly.Parse(req.StartTime);
                })
                .WithMessage(ValidationMessages.MEMBER_ARIVAL_TIME)
                .When(x => !string.IsNullOrWhiteSpace(x.ArrivalTimeOfMember));

            RuleFor(x => x.DisplayName)
                .MaximumLength(55).WithMessage(string.Format(ValidationMessages.MAXLENGTH, "DisplayName", 55))
                .When(x => !string.IsNullOrWhiteSpace(x.DisplayName));

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage(string.Format(ValidationMessages.MAXLENGTH, "Notes", 500))
                .When(x => !string.IsNullOrWhiteSpace(x.Notes));
        }
    }
}
