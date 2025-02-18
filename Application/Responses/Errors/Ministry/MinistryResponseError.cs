using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
namespace Application.Responses.Errors.Ministry
{
    public static class MinistryResponseError
    {
        public static ApiErrorResponseModel NameExist()
        {
            return new ApiErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationErrors = [MinistryError.NAME_EXISTS.Code],
                Message = MinistryError.NAME_EXISTS.Message,
                StatusDescription = ApiResponseErrorMessage.BAD_REQUEST.Message
            };
        }
    }
}
