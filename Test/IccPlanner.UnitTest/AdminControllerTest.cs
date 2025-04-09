using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Responses.Errors;
using FluentAssertions;
using IccPlanner.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Test.IccPlanner.UnitTest
{
    public class AdminControllerTest
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAccountService _accountService;

        private readonly AdminController _adminController;

        public AdminControllerTest()
        {
            _logger = Substitute.For<ILogger<AdminController>>();
            _accountService = Substitute.For<IAccountService>();

            _adminController = new AdminController(_logger, _accountService);
        }

        [Fact]
        public async Task CreateAdminAccount_WhenIsAdminUserExist_ShouldReturnBadRequestError()
        {
            //Arrange
             _accountService.IsAdminExistsAsync().Returns(Task.FromResult(true));

             var request = new CreateAccountRequest
             {
                 Email = "Test@gmail.com",
                 Name = "name",
                 Password = "Password123@",
                 ConfirmPassword = "Password123@",
                 Sexe = "M"
             };

            //Act
            var result = await _adminController.CreateAdminAccount(request);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result).Value;            
           // badRequestResult.Should().BeEquivalentTo(AccountResponseError.AdminUserExist());
        }
         

        [Fact]
        public async Task CreateAdminAccount_WhenThereIsAnyError_ShouldReturnBadRequest()
        {
            //Arrange
            _accountService.IsAdminExistsAsync().Returns(Task.FromResult(false));

            var request = new CreateAccountRequest
            {
                Email = "Test@gmail.com",
                Name = "name",
                Password = "Password123@",
                ConfirmPassword = "Password123@",
                Sexe = "M"
            };
            _accountService.CreateAccount(request).Returns(Task.FromResult(IdentityResult.Failed()));

            //Act
            var result = await _adminController.CreateAdminAccount(request);

            //Assert   
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
