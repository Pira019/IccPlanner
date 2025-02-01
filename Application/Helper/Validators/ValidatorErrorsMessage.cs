
using Domain.Abstractions;

namespace Application.Helper.Validators
{
    public static class ValidatorErrorsMessage
    {
        public static readonly Error LETTERS_ONLY_ATTRIBUTE  = new ("LETTERS_ONLY_ATTRIBUTE", "The field must contain only letters.");
    }
}
