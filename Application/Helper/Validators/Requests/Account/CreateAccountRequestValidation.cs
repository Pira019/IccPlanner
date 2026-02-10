using Application.Requests.Account;
using FluentValidation;
using Shared.Abstraction;
using Shared.Ressources;
namespace Application.Helper.Validators.Requests.Account
{
    public class CreateAccountRequestValidation : AbstractValidator<CreateInvAccountRequest>
    {
        public CreateAccountRequestValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop; 

            RuleFor(x => x.InvitationId)
           .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.Invitation);
             
            // First Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.FIRSTNAME)             
                .MaximumLength(55).WithMessage(ValidationMessages.MAX_LENGTH).WithName(ValidationMessages.FIRSTNAME);

            // Last Name
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.NAME)
                .MaximumLength(55).WithMessage(ValidationMessages.MAX_LENGTH).WithName(ValidationMessages.NAME); 

            // Sexe
            RuleFor(x => x.Sexe)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.SEX)
                .Must(x => x == "M" || x == "F").WithMessage(ValidationMessages.SEXE_IVALID);

            // Téléphone (optionnel)
            RuleFor(x => x.Tel)
                .Matches(@"^\+?[0-9]{7,15}$").WithMessage(string.Format(ValidationMessages.INVALID_VALUE, ValidationMessages.EMAIL))
                .When(x => !string.IsNullOrEmpty(x.Tel));

            // Ville (optionnel)
            RuleFor(x => x.City)
                .MaximumLength(55).WithMessage(ValidationMessages.MAX_LENGTH).WithName(ValidationMessages.CITY)
                .When(x => !string.IsNullOrEmpty(x.City));

            // Secteur (optionnel)
            RuleFor(x => x.Quarter)
               .MaximumLength(55).WithMessage(ValidationMessages.MAX_LENGTH).WithName(ValidationMessages.QUARTER)
                .When(x => !string.IsNullOrEmpty(x.Quarter));

            // Mot de passe
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.PASSWORD) 
                .MinimumLength(8).WithMessage(ValidationMessages.MIN_LENGHT).WithName(ValidationMessages.PASSWORD)
                .Matches(@"[A-Z]").WithMessage(ValidationMessages.MUST_UPPER).WithName(ValidationMessages.PASSWORD)
                .Matches(@"[0-9]").WithMessage(ValidationMessages.MUST_DIGIT).WithName(ValidationMessages.PASSWORD)
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage(ValidationMessages.PASSWORD_INVALID);

            // Confirmation mot de passe
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.PASSWORD_CONFIRM)
                .Equal(x => x.Password).WithMessage(ValidationMessages.PASSWORD_CONFIRM_INVALID);
        }
    } 
}
