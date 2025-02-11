using Application.Constants;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace Test.Infrastructure.UnitTest.Repositories
{
    public class AccountRepositoryTest
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly AccountRepository _accountRepository;

        public AccountRepositoryTest()
        {
            // Substitute for UserManager and SignInManager
            var userStore = Substitute.For<IUserStore<User>>();

            _userManager = Substitute.For<UserManager<User>>(userStore, null, null, null, null, null, null, null, null);
            _signInManager = Substitute.For<SignInManager<User>>(_userManager, Substitute.For<IHttpContextAccessor>(), Substitute.For<IUserClaimsPrincipalFactory<User>>(), null, null, null, null);

            _accountRepository = new AccountRepository(_userManager, _signInManager);
        }

        [Fact]
        public async Task AddUserRole_WhenCalled_ShouldCallAddToRoleAsync()
        {
            //Arrange
            var user = new User
            {
                Email = "Test@gmail.com",
                UserName = "Test"
            };

            _userManager.AddToRoleAsync(Arg.Any<User>(), RolesConstants.ADMIN)
                .Returns(Task.FromResult(IdentityResult.Success));

            //Act
            await _accountRepository.AddUserRole(user, RolesConstants.ADMIN);

            //Assert   
            await _userManager.Received(1).AddToRoleAsync(user, RolesConstants.ADMIN);
        }

        /// <summary>
        /// Doit appeler GetRolesAsync de lòbjet <see cref="UserManager{TUser}"/>
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUserRoles_ShouldCallAGetRolesAsync()
        {
            //Arrange
            var user = new User
            {
                Email = "Test@gmail.com",
                UserName = "Test"
            };

            var roles = new List<string>
            {
                "Admin",
            };

            _userManager.GetRolesAsync(user).Returns(Task.FromResult<IList<string>>(roles));

            //Act
            var response = await _accountRepository.GetUserRoles(user);

            //Assert
            response.Should().BeEquivalentTo(roles);

        }

        [Fact]
        public async Task ConfirmAccountEmailAsync_ShouldReturnIdentityResult()
        {
            //Arrange
            var user = new User
            {
                Email = "Test@gmail.com",
                UserName = "Test"
            };

            var token = "Token";
            var identityResponse = IdentityResult.Success;
            _userManager.ConfirmEmailAsync(user, token).Returns(Task.FromResult(identityResponse));

            //Act
            var response = await _accountRepository.ConfirmAccountEmailAsync(user, token);

            //Assert
            response.Should().Be(identityResponse);

        }

    }
}
