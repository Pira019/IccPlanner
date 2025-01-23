
using Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Helper
{
    /// <summary>
    ///   Helper de retour
    /// </summary>
    public class ApiResponseHelper
    {
        public static ApiErrorResponse CreateValidationErrorResponse (ActionContext context)
        {
            var validationErrors = context.ModelState
            .Where(ms => ms.Value.Errors.Count > 0)
            .SelectMany(ms => ms.Value.Errors.Select(e => e.ErrorMessage))
            .ToArray();

            return new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                StatusDescription = "Validation Failed.",
                Message = "One or more validation errors occurred.",
                ValidationErrors = validationErrors
            };
        }
    }
}
