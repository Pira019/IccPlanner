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

            RuleFor(x => x.DisplayName)
               .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DISPLAY_NAME);

            RuleFor(x => x.MemberArrivalTime)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.PROGRAM)
               .Must((model, arrivalTime) =>
               {
                   if (string.IsNullOrWhiteSpace(arrivalTime))
                       return true; 
                   return SharedUtiles.BeAValidTimeOnly(arrivalTime);

               }).WithMessage(ValidationMessages.INVALID_ENTRY).WithName(ValidationMessages.MEMBER_ARRIVAL);    
        }
    }
}
