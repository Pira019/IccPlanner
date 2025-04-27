using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Ressources;

namespace Application.Responses.Errors
{
    public class ApiError
    {
        /// <summary>
        ///  Model de message d'erreur de retour 
        /// </summary>
        /// <param name="identityResult"></param>
        /// <returns>
        /// <see cref="Errors.ApiError"/>
        /// </returns>
        public static ApiErrorResponseModel ApiIdentityResultResponseError(IdentityResult? identityResult = null)
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = identityResult?.Errors.Select(e => e.Description).ToArray() ?? [ApiResponseErrorMessage.ERROR_UNDEFINED.Code],
                Message = identityResult?.ToString() ?? ApiResponseErrorMessage.ERROR_UNDEFINED.Message,
                StatusDescription = ApiResponseErrorMessage.BAD_REQUEST.Message
            };
        }

        /// <summary>
        /// Définit l'erreur inconnu 
        /// </summary>
        /// <param name="message"> Message personnalisé</param>
        /// <returns> <see cref="ApiErrorResponseModel"/> représente le model d'erreur de retour</returns>
        public static ApiErrorResponseModel InternalServerError(string? message = null)
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status500InternalServerError, 
                Message = message ?? ApiResponseErrorMessage.ERROR_UNDEFINED.Message,
                StatusDescription = ApiResponseErrorMessage.ERROR_UNDEFINED.Message
            };
        }

        /// <summary>
        /// Définit l'erreur d'authentification
        /// </summary>
        /// <param name="isUnauthorized">Paramètre</param>
        /// <returns> <see cref="ApiErrorResponseModel"/> représente le model d'erreur de retour</returns>
        public static ApiErrorResponseModel AuthError(bool isUnauthorized = false)
        {
            return new ApiErrorResponseModel
            {
                Success = false,
                StatusCode = isUnauthorized ? StatusCodes.Status401Unauthorized : StatusCodes.Status403Forbidden,
                Message = isUnauthorized ? ValidationMessages.UNAUTHORIZED : ValidationMessages.FORBIDDEN_ACCESS,
                StatusDescription = isUnauthorized ? StatusCodes.Status401Unauthorized.ToString() : StatusCodes.Status403Forbidden.ToString()
            };
        }

        /// <summary>
        /// Retour le message d'erreur 
        /// </summary>
        /// <param name="errorMessage">
        /// Le message d'erreur 
        /// </param>
        /// <param name="propertyName">
        /// Nom de la propriété
        /// </param>
        /// <param name="propertyValue">
        /// Valuer
        /// </param>
        /// <returns></returns>
        public static ApiErrorResponseModel ErrorMessage(string errorMessage, string? propertyName, string? propertyValue)
        {
            var formattageMessage = errorMessage
                .Replace("{PropertyName}", propertyName)
                .Replace("{PropertyValue}", propertyValue);

            return new ApiErrorResponseModel
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = null,
                Message = formattageMessage,
                StatusDescription = ValidationMessages.BAD_REQUEST
            };
        }
    }
}
