
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Responses.Errors
{
    /// <summary>
    /// Cette classe permet de gerer le retour des erreur des actons d'un compte
    /// </summary>
    public class AccountResponseError
    {
        /// <summary>
        ///  Model de message d'erreur de retour dans la creation de compte
        /// </summary>
        /// <param name="identityResult"></param>
        /// <returns>
        /// <see cref="ApiErrorResponse"/>
        /// </returns>
        public static ApiErrorResponseModel ApiErrorResponse(IdentityResult identityResult) 
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = identityResult.Errors.Select(e => e.Description).ToArray() ?? [ApiResponseErrorMessage.ERROR_UNDEFINED.Code],
                Message = identityResult.ToString() ?? ApiResponseErrorMessage.ERROR_UNDEFINED.Message,
                StatusDescription = ApiResponseErrorMessage.BAD_REQUEST.Message
            };                
        }

        
        public static ApiErrorResponseModel UserNotFound()
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = [AccountErrors.USER_NOT_FOUND.Code],
                Message = AccountErrors.USER_NOT_FOUND.Message,
                StatusDescription = ApiResponseErrorMessage.BAD_REQUEST.Message
            };
        }
    }
}
