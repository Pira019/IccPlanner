using System.ComponentModel.DataAnnotations;
using Application.Helper.Validators;

public class EndDateAfterStartDateAttribute : ValidationAttribute
{
    private readonly string _startDatePropertyName;

    public EndDateAfterStartDateAttribute(string startDatePropertyName)
    {
        _startDatePropertyName = startDatePropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var instance = validationContext.ObjectInstance;
        var type = instance.GetType();
        var startDateProperty = type.GetProperty(_startDatePropertyName);

        if (startDateProperty == null)
            return new ValidationResult($"Propriété {_startDatePropertyName} introuvable.");

        var startDateValue = (DateOnly?)startDateProperty.GetValue(instance);
        var endDateValue = (DateOnly?)value;

        if (startDateValue.HasValue && endDateValue.HasValue)
        {
            if (endDateValue.Value < startDateValue.Value)
            {
                return new ValidationResult(ValidatorErrorsMessage.END_DATE_MUST_BE_GREATER.Message);
            }
        } 
        return ValidationResult.Success;
    }
}
