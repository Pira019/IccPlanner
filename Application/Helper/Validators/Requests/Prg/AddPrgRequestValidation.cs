// Ignore Spelling: Prg

using Application.Interfaces.Services;
using Application.Requests.Program; 
using FluentValidation;
using Shared.Ressources;

namespace Application.Helper.Validators.Requests.Prg
{
    /// <summary>
    /// Validation du body <see cref="AddProgramRequest"/> lors de l'ajout d'un programme 
    /// </summary>
    public class AddPrgRequestValidation : AbstractValidator<AddProgramRequest>
    {
        public AddPrgRequestValidation(IProgramService programService) 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.PROGRAM_NAME)
                .MaximumLength(255).WithMessage(ValidationMessages.MAX_LENGTH).WithName(ValidationMessages.PROGRAM_NAME);

            RuleFor(X => X.Description)
              .NotEmpty().WithMessage(ValidationMessages.NOT_NULL).WithName(ValidationMessages.DESCRIPTION);
        }
    }
}
