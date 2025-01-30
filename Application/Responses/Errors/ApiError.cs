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
        public static ApiErrorResponseModel ApiErrorResponse(IdentityResult? identityResult = null)
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = identityResult?.Errors.Select(e => e.Description).ToArray() ?? [ApiResponseErrorMessage.ERROR_UNDEFINED.Code],
                Message = identityResult?.ToString() ?? ApiResponseErrorMessage.ERROR_UNDEFINED.Message,
                StatusDescription = ApiResponseErrorMessage.BAD_REQUEST.Message
            };
        }
    }
}
