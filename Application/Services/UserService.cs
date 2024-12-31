
using Application.Dtos.UserDTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests.User;
using AutoMapper;
using Domain.Entities;
using PasswordGenerator;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void CreateUser(CreateUserRequest createUserRequest)
        {
            var dto = _mapper.Map<CreateUserDTO>(createUserRequest);
            dto.Password = new Password().Next();

            var newUser = new User
            {
                Password = dto.Password,
                Member = dto.Member,
            }; 
            _repository.insert(newUser);
        }


    }
}
