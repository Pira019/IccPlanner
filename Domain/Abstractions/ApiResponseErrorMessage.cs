
using Shared.Ressources;

namespace Domain.Abstractions
{
    /// <summary>
    /// Cette classe définit le message d'erreur de l'API
    /// </summary>
    public static class ApiResponseErrorMessage
    {
        static public readonly Error BAD_REQUEST = new ("404", ValidationMessages.BAD_REQUEST);
        static public readonly Error ERROR_UNDEFINED = new ("ERROR_UNDEFINED", ValidationMessages.ERROR_UNDEFINED);
        static public readonly Error VALIDATION_FAILED = new ("VALIDATION_FAILED", ValidationMessages.VALIDATION_FAILED); 
        static public readonly Error UNAUTHORIZED = new ("UNAUTHORIZED", ValidationMessages.UNAUTHORIZED); 
        static public readonly Error FORBIDDEN_ACCESS = new ("FORBIDDEN_ACCESS", ValidationMessages.FORBIDDEN_ACCESS); 
    }
}
