using Application.Interfaces.Services;
using Application.Requests.Ministry;
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests
{
    public class MjMinistryRequestValidation : AbstractValidator<AddMinistryRequest>
    { 
        public MjMinistryRequestValidation() 
        { 
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(X => X.Name)
                .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.NAME)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.NAME)
                .MaximumLength(255).WithMessage(string.Format(ValidationMessages.MAXLENGTH, ValidationMessages.MINISTRY_NAME, 255));

            RuleFor(X => X.Description)
                .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION); 

            RuleFor(X => X.Id)
                .NotNull().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.Id)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.Id); 
           
        } 
    }
}
