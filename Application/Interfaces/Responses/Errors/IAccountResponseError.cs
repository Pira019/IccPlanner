using Application.Responses;

namespace Application.Interfaces.Responses.Errors
{
    public interface IAccountResponseError
    {
        public ApiErrorResponseModel LoginInvalidAttempt();
        public ApiErrorResponseModel UserNotFound();
        public ApiErrorResponseModel UserIsLockedOut();
        public ApiErrorResponseModel AdminUserExist();
    }
}
