using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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
        /// Definit l'erreur inconnu 
        /// </summary>
        /// <param name="message"> Message personaliser</param>
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
        /// Definit l'erreur d'authentification
        /// </summary>
        /// <param name="isUnauthorized">Paramettre</param>
        /// <returns> <see cref="ApiErrorResponseModel"/> représente le model d'erreur de retour</returns>
        public static ApiErrorResponseModel AuthError(bool isUnauthorized = false)
        {
            return new ApiErrorResponseModel
            {
                StatusCode = isUnauthorized ? StatusCodes.Status401Unauthorized : StatusCodes.Status403Forbidden,
                Message = isUnauthorized ? ApiResponseErrorMessage.UNAUTHORIZED.Message : ApiResponseErrorMessage.FORBIDDEN_ACCESS.Message,
                StatusDescription = isUnauthorized ? ApiResponseErrorMessage.UNAUTHORIZED.Message : ApiResponseErrorMessage.FORBIDDEN_ACCESS.Message
            };
        }
    }
}
