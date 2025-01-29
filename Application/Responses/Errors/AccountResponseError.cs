
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Responses.Errors
{
    /// <summary>
    /// Cette classe permet de gerer le retour des erreur des actons d'un compte
    /// </summary>
    public class AccountResponseError
    {
        public static readonly ILogger<AccountResponseError>? _logger;

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
            _logger?.LogWarning(AccountErrors.USER_NOT_FOUND.Message); 

            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = [AccountErrors.USER_NOT_FOUND.Code],
                Message = AccountErrors.USER_NOT_FOUND.Message,
                StatusDescription = ApiResponseErrorMessage.BAD_REQUEST.Message
            };
        }
        
        /// <summary>
        /// Retourn l'erreur login invalid
        /// </summary>
        /// <returns>
        /// Le model d'erreur <see cref="ApiErrorResponseModel"/>
        /// </returns>
        public static ApiErrorResponseModel LoginInvalidAttempt()
        {
            _logger?.LogWarning(AccountErrors.INVALID_LOGIN_ATTEMPT.Message);

            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = [AccountErrors.INVALID_LOGIN_ATTEMPT.Code],
                Message = AccountErrors.INVALID_LOGIN_ATTEMPT.Message,
                StatusDescription = ApiResponseErrorMessage.BAD_REQUEST.Message
            };
        }

        /// <summary>
        /// Retourne le bon message quand un compte est bloqué
        /// </summary>
        /// <returns>
        /// Retourn l'erreur login invalid quand un utilisateur est bloqué
        /// Le model d'erreur <see cref="ApiErrorResponseModel"/>
        /// </returns>
        public static ApiErrorResponseModel UserIsLockedOut()
        {
            _logger?.LogWarning(AccountErrors.USER_IS_LOCKED_OUT.Message);

            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = [AccountErrors.USER_IS_LOCKED_OUT.Code],
                Message = AccountErrors.USER_IS_LOCKED_OUT.Message,
                StatusDescription = ApiResponseErrorMessage.BAD_REQUEST.Message
            };
        }
    }
}
