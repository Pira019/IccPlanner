using Application.Dtos.Account;
using Application.Dtos.Department;
using Application.Dtos.UserDTOs;
using Application.Requests.Account;
using Application.Requests.Department;
using AutoMapper;
using Domain.Entities;

namespace Test.Application.UnitTest.Dtos.AccountTest
{
    public class AccountMappingProfileTest
    {
        private readonly IMapper _mapper;

        public AccountMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountMappingProfile>();
            });

            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_CreateAccountRequest_To_CreateAccountDto()
        {
            var request = new CreateAccountRequest
            {
               ConfirmPassword = "Test" ,
               Email = "Test@gmail.com",
               Name = "Test" ,
               Password = "Test" ,
               Sexe = "M" 
            };

            var result = _mapper.Map<CreateAccountDto>(request);

            Assert.NotNull(result);
            Assert.Equal(request.Name, result.User.Member.Name); 
        }
    }
}
