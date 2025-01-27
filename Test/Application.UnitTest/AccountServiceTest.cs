using Application.Dtos.Account;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions; 
using Microsoft.AspNetCore.Identity;
using NSubstitute;  

namespace Test.Application.UnitTest
{
    public class AccountServiceTest
    { 
         private readonly IAccountRepository _accountRepository;
         private readonly ISendEmailService _sendEmailService;
         private readonly IMapper _mapper;

        private readonly AccountService accountService;

        public AccountServiceTest()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
            _sendEmailService = Substitute.For<ISendEmailService>();
            _mapper = Substitute.For<IMapper>();

            accountService = new AccountService(_accountRepository, _mapper, _sendEmailService);
        }

        [Fact]
        public async Task CreateAccount_Should_ReturnIdentityResult_WhenCreateAccountSucceffully()
        {
            // Arrange
            var createAccouteRequest = new CreateAccountRequest
            {
                Email = "Test@gmail.com",
                Name = "name",
                Password = "Password123@",
                ConfirmPassword = "Password123@",
                Sexe = "M"
            };

            var createAccountDto = new CreateAccountDto
            { 
                User = new User
                {
                    Email = createAccouteRequest.Email, 
                    PasswordHash = createAccouteRequest.Password,
                    UserName = createAccouteRequest.Email,
                    PhoneNumber = createAccouteRequest.Tel, 
                    //Creation du membre
                    Member = new Member
                    {
                        Name = createAccouteRequest.Name,
                        LastName = createAccouteRequest.LastName, 
                        Quarter = createAccouteRequest.Quarter,
                        City = createAccouteRequest.City,
                        
                    }
                }
            };

            _mapper.Map<CreateAccountDto>(createAccouteRequest).Returns(createAccountDto);

            var identityResult = IdentityResult.Success;
            _accountRepository.CreateAsync(Arg.Any<User>(), Arg.Any<String>()).Returns(identityResult);
            _accountRepository.FindByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<User?>(createAccountDto.User));


            //Act
            var resul = await accountService.CreateAccount(createAccouteRequest);

           //Assert
           resul.Should().Be(identityResult);
            await _sendEmailService.Received(1).SendEmailConfirmation(createAccountDto.User);
        }
    }
}
