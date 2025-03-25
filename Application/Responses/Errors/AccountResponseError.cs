using Domain.Abstractions;
using Microsoft.AspNetCore.Http; 

namespace Application.Responses.Errors
{
    /// <summary>
    /// Cette classe permet de gérer le retour des erreur des actons d'un compte
    /// </summary>
    public class AccountResponseError : ApiError
    {
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

        /// <summary>
        /// Retourne l'erreur login invalid
        /// </summary>
        /// <returns>
        /// Le model d'erreur <see cref="ApiErrorResponseModel"/>
        /// </returns>
        public static ApiErrorResponseModel LoginInvalidAttempt()
        {
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
        /// Retourné l'erreur login invalid quand un utilisateur est bloqué
        /// Le model d'erreur <see cref="ApiErrorResponseModel"/>
        /// </returns>
        public static ApiErrorResponseModel UserIsLockedOut()
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = [AccountErrors.USER_IS_LOCKED_OUT.Code],
                Message = AccountErrors.USER_IS_LOCKED_OUT.Message,
                StatusDescription = ApiResponseErrorMessage.BAD_REQUEST.Message
            };
        }

        public static ApiErrorResponseModel AdminUserExist()
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status403Forbidden, 
                Message = AccountErrors.ADMIN_USER_EXIST.Message,
                StatusDescription = StatusCodes.Status403Forbidden.ToString()
            };
        }
    }
}
