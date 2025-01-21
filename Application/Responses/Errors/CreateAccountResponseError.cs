
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Responses.Errors
{
    /// <summary>
    /// Cette classe permet de gerer le retrour pour créer un compte
    /// </summary>
    public class CreateAccountResponseError
    {
        /// <summary>
        ///  Model de message d'erreur de retour dans la creation de compte
        /// </summary>
        /// <param name="identityResult"></param>
        /// <returns></returns>
        public static ApiErrorResponse ApiErrorResponse(IdentityResult identityResult) 
        {
            return new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = identityResult.Errors.Select(e => e.Description).ToArray(),
                Message = identityResult.ToString(),
                StatusDescription = "Bad request",
            };                
        }
    }
}
