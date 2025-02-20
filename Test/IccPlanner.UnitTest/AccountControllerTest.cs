using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Responses.Account;
using Application.Responses.Errors; 
using Domain.Entities;
using FluentAssertions;
using IccPlanner.Controllers;
using Infrastructure.Configurations;
using Infrastructure.Configurations.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Test.IccPlanner.UnitTest
{
    /// <summary>
    /// Test AccountsController
    /// </summary>
    public class AccountControllerTest
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountService _accountService;

        private readonly AccountsController _accountController;

        public AccountControllerTest()
        {
            _logger = Substitute.For<ILogger<AccountsController>>();
            _accountService = Substitute.For<IAccountService>();

            _accountController = new AccountsController(_accountService, _logger);
        }

        [Fact]
        public async Task Login_WhenUserIsLockedOut_ShouldReturnBadRequest()
        {
            //Arrange
            var signInResult = Microsoft.AspNetCore.Identity.SignInResult.LockedOut;

            var config = Substitute.For<IConfiguration>();
            var tokenProvider = Substitute.For<TokenProvider>(config);

            var requestData = new LoginRequest
            {
                Email = "Test@gmail.com",
                Password = "password",
                Remember = true,
            };

            _accountService.Login(requestData).Returns(Task.FromResult(signInResult));

            //Act
            var requestController = await _accountController.Login(requestData, tokenProvider);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(requestController).Value;
            badRequestResult.Should().BeEquivalentTo(AccountResponseError.UserIsLockedOut());
        }
        
        [Fact]
        public async Task Create_WhenNotSucceed_ShouldReturnBadRequest()
        {
            //Arrange
            var identity = IdentityResult.Failed();
            var request = new CreateAccountRequest
            {
                ConfirmPassword = "TEST",
                Email = "Test@gmail.com",
                Name = "name",
                Password = "password",
                Sexe = "M"
            };
            
            _accountService.CreateAccount(request).Returns(Task.FromResult(identity)); 

            //Act
            var requestController = await _accountController.Create(request);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(requestController).Value;
            badRequestResult.Should().BeEquivalentTo(AccountResponseError.ApiIdentityResultResponseError(identity));
        }
        
        [Fact]
        public async Task Create_WhenSucceed_ShouldReturnBadRequest()
        {
            //Arrange
            var identity = IdentityResult.Success;
            var request = new CreateAccountRequest
            {
                ConfirmPassword = "TEST",
                Email = "Test@gmail.com",
                Name = "name",
                Password = "password",
                Sexe = "M"
            };
            
            _accountService.CreateAccount(request).Returns(Task.FromResult(identity)); 

            //Act
            var requestController = await _accountController.Create(request);

            //Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(requestController);
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task Login_WhenUserINotSucceed_ShouldReturnBadRequest()
        {
            //Arrange
            var signInResult = Microsoft.AspNetCore.Identity.SignInResult.Failed;

            var config = Substitute.For<IConfiguration>();
            var tokenProvider = Substitute.For<TokenProvider>(config);

            var requestData = new LoginRequest
            {
                Email = "Test@gmail.com",
                Password = "password",
                Remember = true,
            };

            _accountService.Login(requestData).Returns(Task.FromResult(signInResult));

            //Act
            var requestController = await _accountController.Login(requestData, tokenProvider);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(requestController).Value;
            badRequestResult.Should().BeEquivalentTo(AccountResponseError.LoginInvalidAttempt());
        }

        [Fact]
        public async Task Login_WhenSucceed_ShouldReturnOkObjectResult()
        {
            //Arrange
            var signInResult = Microsoft.AspNetCore.Identity.SignInResult.Success;

            var tokenProvider = Substitute.For<ITokenProvider>();

            var requestData = new LoginRequest
            {
                Email = "Test@gmail.com",
                Password = "password",
                Remember = true,
            };

            var rolesName = new List<string>
            {
                "Admin",
            };

            var fakeUser = new User { Email = requestData.Email };

            _accountService.Login(requestData).Returns(Task.FromResult(signInResult));
            _accountService.FindUserAccountByEmail(requestData.Email).Returns(Task.FromResult<User?>(fakeUser));
            _accountService.GetUserRoles(Arg.Any<User>()).Returns(Task.FromResult<ICollection<string>>(rolesName));

            var token = "token";

            tokenProvider.Create(Arg.Any<User>(), Arg.Any<ICollection<string>>()).Returns(token);
            //Act
            var response = await _accountController.Login(requestData, tokenProvider);
            var resultAct = new LoginAccountResponse
            { AccessToken = token };


            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<LoginAccountResponse>(okResult.Value);
            okResult.Value.Should().BeEquivalentTo(resultAct);
        }

        [Fact]
        public async void ConfirmEmail_WhenAccountIdNotFound_ShouldReturnBadRequestObjectResult()
        {
            //Arrange
            var request = new ConfirmEmailRequest
            {
                Token = "TokenFake",
                UserId = "idUser"
            };

            _accountService.FindUserAccountById(request.UserId).Returns(Task.FromResult<User?>(null)); 

            //Act
            var response = await _accountController.ConfirmEmail(request);

            //Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(response);
            badRequest.Value.Should().BeEquivalentTo(AccountResponseError.UserNotFound());
        }

        [Fact]
        public async void ConfirmEmail_WhenIsSucceed_ShouldReturnOkObjectResult()
        {
            //Arrange
            var request = new ConfirmEmailRequest
            {
                Token = "TokenFake",
                UserId = "idUser"
            };

            var user = new User
            {
                Id = "idUser",
                Email = "Test@gmail.com"
            };

            var identitySucceed = IdentityResult.Success;

            _accountService.FindUserAccountById(request.UserId).Returns(Task.FromResult<User?>(user));
            _accountService.ConfirmEmailAccount(user, request.Token).Returns(Task.FromResult(identitySucceed));

            //Act
            var response = await _accountController.ConfirmEmail(request);

            //Assert
            var okResult = Assert.IsType<OkResult>(response); 
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void ConfirmEmail_WhenIsFailled_ShouldReturnBadRequestObjectResult()
        {
            //Arrange
            var request = new ConfirmEmailRequest
            {
                Token = "TokenFake",
                UserId = "idUser"
            };

            var user = new User
            {
                Id = "idUser",
                Email = "Test@gmail.com"
            };

            var identityFailled = IdentityResult.Failed();

            _accountService.FindUserAccountById(request.UserId).Returns(Task.FromResult<User?>(user));
            _accountService.ConfirmEmailAccount(user, request.Token).Returns(Task.FromResult(identityFailled));

            //Act
            var response = await _accountController.ConfirmEmail(request);

            //Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(response).Value;
            okResult.Should().BeEquivalentTo(AccountResponseError.ApiIdentityResultResponseError(identityFailled));
        }
    }
}
