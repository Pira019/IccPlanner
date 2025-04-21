
using Application.Responses;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Ressources;

namespace Application.Helper
{
    /// <summary>
    ///   Helper de retour
    /// </summary>
    public static class ApiResponseHelper
    {
        public static ApiErrorResponseModel CreateValidationErrorResponse (ActionContext context)
        {
            var validationErrors = context.ModelState
            .Where(ms => ms.Value?.Errors.Count > 0)
            .SelectMany(ms => ms.Value?.Errors.Select(e => e.ErrorMessage)!)
            .ToArray();

            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                StatusDescription = StatusCodes.Status400BadRequest.ToString(),
                Message = ValidationMessages.VALIDATION_FAILED,
                ValidationErrors = validationErrors
            };
        }
    }
}
