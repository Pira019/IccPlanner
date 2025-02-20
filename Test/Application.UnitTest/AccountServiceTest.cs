using System.Text;
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
using Microsoft.AspNetCore.WebUtilities;
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
        public async Task CreateAccount_ShouldReturnIdentityResult_WhenCreateAccountSucceed()
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
            _accountRepository.CreateAsync(createAccountDto.User, createAccouteRequest.Password).Returns(identityResult);
            _accountRepository.FindByEmailAsync(createAccouteRequest.Email).Returns(Task.FromResult<User?>(createAccountDto.User));


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

            _accountRepository.CreateAsync(createAccountDto.User, createAccouteRequest.Password).Returns(identityResult);
            _accountRepository.FindByEmailAsync(createAccouteRequest.Email).Returns(Task.FromResult<User?>(createAccountDto.User));

            //Act 
            var result = await accountService.CreateAccount(createAccouteRequest, true);

            //Assert
            result.Should().Be(identityResult);
            await _accountRepository.Received(1).AddUserRole(Arg.Is<User>(u => u.Email == createAccouteRequest.Email), RolesConstants.ADMIN);
        }

        [Fact]
        public async void CreateAccount_WhenNotSucceed_ShouldReturnIdentityResult()
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

            var identityResult = IdentityResult.Failed();

            _accountRepository.CreateAsync(createAccountDto.User!, createAccouteRequest.Password!).Returns(Task.FromResult(identityResult));

            //Act 
            var result = await accountService.CreateAccount(createAccouteRequest);

            //Assert
            result.Should().Be(identityResult);
            await _sendEmailService.Received(0).SendEmailConfirmation(createAccountDto.User);
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

        [Fact]
        public async Task ConfirmEmailAccount_ShouldReturnIdentityResult()
        {
            //Arrange
            var user = new User
            {
                Email = "Test@gmail.com"
            };

            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode("test"));

            var identityResult = IdentityResult.Success;

            _accountRepository.ConfirmAccountEmailAsync(user, code).Returns(Task.FromResult(identityResult));

            //Act
            var response = await accountService.ConfirmEmailAccount(user, "test");

            //Assert
            response.Should().Be(identityResult);
        }

        [Fact]
        public async Task IsAdminExistsAsync_ShouldReturnTrue()
        {
            //Arrange 

            _accountRepository.IsAdminExistsAsync().Returns(Task.FromResult(true));

            //Act
            var response = await accountService.IsAdminExistsAsync();

            //Assert
            response.Should().Be(true);
        }

        [Fact]
        public async Task Login_ShouldReturnTrue()
        {
            //Arrange 

            var loginRequest = new LoginRequest
            {
                Email = "Test@gmail.com",
                Password = "password",
                Remember = false
            };

            var singInResult = SignInResult.Success;

            _accountRepository.SignIn(loginRequest.Email, loginRequest.Password, loginRequest.Remember).Returns(Task.FromResult(singInResult));

            //Act
            var response = await accountService.Login(loginRequest);

            //Assert
            response.Should().Be(singInResult);
        }

        [Fact]
        public async Task FindUserAccountById_ShouldReturnUser()
        {
            //Arrange 
            var user = new User
            {
                Id = "1"
            };

            _accountRepository.FindByIdAsync(user.Id).Returns(Task.FromResult(user ?? null));

            //Act
            var response = await accountService.FindUserAccountById("1");

            //Assert
            response.Should().Be(user);
        }
    }
}
