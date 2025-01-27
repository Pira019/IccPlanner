 
namespace Domain.Abstractions
{
    /// <summary>
    /// Cette classe definit le message d'erreur de l'API
    /// </summary>
    public class ApiResponseErrorMessage
    {
        static public readonly Error BAD_REQUEST = new ("404", "Bad request.");
        static public readonly Error ERROR_UNDEFINED = new ("ERROR_UNDEFINED", "Undefined Error.");
    }
}
