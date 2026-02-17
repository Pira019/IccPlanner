// Ignore Spelling: Prg Validators

using Application.Requests.ServiceTab;
using FluentValidation;
using Shared.Enums;
using Shared.Ressources;
using Shared.Utiles;

namespace Application.Helper.Validators.Requests.ServieTab
{
    public class AddServicePrgDepartmentValidation : AbstractValidator<AddServicePrgDepartmentRequest>
    {
        public AddServicePrgDepartmentValidation() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.SERVICE)
                .GreaterThan(0).WithMessage(ValidationMessages.INVALID_ENTRY).WithName(ValidationMessages.SERVICE);

            RuleFor(x => x.PrgDateId)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.PROGRAM)
                .GreaterThan(0).WithMessage(ValidationMessages.INVALID_ENTRY).WithName(ValidationMessages.PROGRAM);
             

            RuleFor(x => x.MemberArrivalTime)
              .Must(SharedUtiles.BeAValidTimeOnly)
              .When(x => !string.IsNullOrWhiteSpace(x.MemberArrivalTime))
              .WithMessage(ValidationMessages.INVALID_ENTRY)
              .WithName(ValidationMessages.MEMBER_ARRIVAL);

        }
    }
}
