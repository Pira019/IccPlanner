
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Helper.Validators
{
    /// <summary>
    /// Personnaliser le message de validation
    /// </summary>
    public class LettersOnlyAttribute : ValidationAttribute
    {
        public LettersOnlyAttribute()
        {
            this.ErrorMessage = ValidatorErrorsMessage.LETTERS_ONLY_ATTRIBUTE.Message;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true; // Si la valeur est nulle, la validation sera considérée comme réussie

            string? strValue = value as string;
            if (strValue == null) return false;

            // Expression régulière pour valider uniquement les lettres
            Regex regex = new Regex(@"^[a-zA-Z]+$");
            return regex.IsMatch(strValue);
        }
    }
}
