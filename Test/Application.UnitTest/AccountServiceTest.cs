using Application.Constants;
using Application.Dtos.Account;
using Application.Interfaces.Repositories;
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
                        Sexe = createAccouteRequest.Sexe

                    }
                }
            };

            _mapper.Map<CreateAccountDto>(createAccouteRequest).Returns(createAccountDto);

            var identityResult = IdentityResult.Success;
            _accountRepository.CreateAsync(Arg.Any<User>(), Arg.Any<String>()).Returns(identityResult);
            _accountRepository.FindByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<User?>(createAccountDto.User));


            //Act
            var result = await accountService.CreateAccount(createAccouteRequest);

            //Assert
            result.Should().Be(identityResult);
            await _sendEmailService.Received(1).SendEmailConfirmation(createAccountDto.User);
        }

        [Fact]
        public async void CreateAccount_WhenIsAdmin_ShouldReturnIdentityResult()
        {
            //Arrange
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
                        Sexe = createAccouteRequest.Sexe

                    }
                }
            };

            _mapper.Map<CreateAccountDto>(createAccouteRequest).Returns(createAccountDto);

            var identityResult = IdentityResult.Success;

            _accountRepository.CreateAsync(Arg.Any<User>(), Arg.Any<String>()).Returns(identityResult);
            _accountRepository.FindByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<User?>(createAccountDto.User));

            //Act 
            var result = await accountService.CreateAccount(createAccouteRequest, true);

            //Assert
            result.Should().Be(identityResult);
            await _accountRepository.Received(1).AddUserRole(Arg.Is<User>(u => u.Email == createAccouteRequest.Email), RolesConstants.ADMIN);

        }

        [Fact]
        public async void GetUserRoles_ShouldReturnListString()
        {
            //Arrange
            var user = new User
            {
                Email = "Test@gmail.com"
            };

            var roleName = new List<string>
            {
                "Admin"
            }; 

            _accountRepository.GetUserRoles(user).Returns(Task.FromResult<IList<string>>(roleName));

            //Act
            var response = await accountService.GetUserRoles(user);

            //Assert
            response.Should().BeEquivalentTo(roleName);
        } 
    }
}
