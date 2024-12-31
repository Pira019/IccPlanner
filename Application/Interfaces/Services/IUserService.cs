

using Application.Requests.User;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        void CreateUser(CreateUserRequest createUserRequest);
    }
}
