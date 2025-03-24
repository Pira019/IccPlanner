
using Domain.Abstractions;

namespace Application.Helper.Validators
{
    public static class ValidatorErrorsMessage
    {
        public static readonly Error LETTERS_ONLY_ATTRIBUTE  = new ("LETTERS_ONLY_ATTRIBUTE", "The field must contain only letters.");
        public static readonly Error END_DATE_MUST_BE_GREATER = new ("END_DATE_MUST_BE_GREATER", "EndAt must be greater than or equal to StartAt.");
    }
}
