using Application.Constants;
using Application.Requests.Account;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
            _signInManager = Substitute.For<SignInManager<User>>(_userManager,Substitute.For<IHttpContextAccessor>(), Substitute.For<IUserClaimsPrincipalFactory<User>>(),null,null,null,null);

            _accountRepository = new AccountRepository(_userManager, _signInManager);
        }

        [Fact]
        public async Task AddUserRole_WhenCalled_ShouldCallAddToRoleAsync()
        {
            //Arrange
            var user = new User
            {
                Email = "Test@gmail.com",
                UserName ="Test"                 
            };

            _userManager.AddToRoleAsync(Arg.Any<User>(), RolesConstants.ADMIN)
                .Returns(Task.FromResult(IdentityResult.Success));

            //Act
            await _accountRepository.AddUserRole(user, RolesConstants.ADMIN);

            //Assert   
            await _userManager.Received(1).AddToRoleAsync(user, RolesConstants.ADMIN);
        }


    }
}
