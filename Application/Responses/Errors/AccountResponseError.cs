using Application.Interfaces.Responses.Errors;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;  
using Shared.Ressources;

namespace Application.Responses.Errors
{
    /// <summary>
    /// Cette classe permet de gérer le retour des erreur des actons d'un compte
    /// </summary>
    public class AccountResponseError : ApiError, IAccountResponseError
    {
        public ApiErrorResponseModel UserNotFound()
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
        public ApiErrorResponseModel LoginInvalidAttempt()
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest, 
                Message = ValidationMessages.INVALID_LOGIN_ATTEMPT,
                StatusDescription = ValidationMessages.BAD_REQUEST,
            };
        }

        /// <summary>
        /// Retourne le bon message quand un compte est bloqué
        /// </summary>
        /// <returns>
        /// Retourné l'erreur login invalid quand un utilisateur est bloqué
        /// Le model d'erreur <see cref="ApiErrorResponseModel"/>
        /// </returns>
        public ApiErrorResponseModel UserIsLockedOut()
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest, 
                Message = ValidationMessages.USER_IS_LOCKED_OUT,
                StatusDescription = ValidationMessages.BAD_REQUEST,
            };
        }

        public ApiErrorResponseModel AdminUserExist()
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
