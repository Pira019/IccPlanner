using Application.Constants;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Test.Infrastructure.UnitTest.Repositories
{
    public class AccountRepositoryTest
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IccPlannerContext _iccPlannerContext;

        private readonly AccountRepository _accountRepository;

        public AccountRepositoryTest()
        {
            // Substitute for UserManager and SignInManager
            var userStore = Substitute.For<IUserStore<User>>();

            _userManager = Substitute.For<UserManager<User>>(userStore, null, null, null, null, null, null, null, null);
            _signInManager = Substitute.For<SignInManager<User>>(_userManager, Substitute.For<IHttpContextAccessor>(), Substitute.For<IUserClaimsPrincipalFactory<User>>(), null, null, null, null);


            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                   .UseInMemoryDatabase("fakeDb")
                   .Options;

            _iccPlannerContext = new IccPlannerContext(option);

            _accountRepository = new AccountRepository(_userManager, _signInManager, _iccPlannerContext);
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
        /// Doit appeler GetRolesAsync de objet <see cref="UserManager{TUser}"/>
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

        [Fact]
        public async Task CreateAsync_ShouldReturnIdentityResult()
        {
            //Arrange
            var user = new User
            {
                Email = "Test@gmail.com",
                UserName = "Test"
            };

            var token = "Token";
            var identityResponse = IdentityResult.Success;
            _userManager.CreateAsync(user, token).Returns(Task.FromResult(identityResponse));

            //Act
            var response = await _accountRepository.CreateAsync(user, token);

            //Assert
            response.Should().Be(identityResponse);

        }

        [Fact]
        public async Task FindByEmailAsync_ShouldReturnUser()
        {
            //Arrange
            var user = new User
            {
                Email = "Test@gmail.com",
                UserName = "Test"
            };

            _userManager.FindByEmailAsync(user!.Email).Returns(Task.FromResult(user ?? null));

            //Act
            var response = await _accountRepository.FindByEmailAsync(user!.Email);

            //Assert
            response.Should().Be(user);

        }

        [Fact]
        public async Task FindByIdAsync_ShouldReturnUser()
        {
            //Arrange
            var user = new User
            {
                Email = "Test@gmail.com",
                UserName = "Test",
                Id = "1"

            };

            _userManager.FindByIdAsync(user!.Id).Returns(Task.FromResult(user ?? null));

            //Act
            var response = await _accountRepository.FindByIdAsync(user!.Id);

            //Assert
            response.Should().Be(user);
        }

        [Fact]
        public async Task IsAdminExistsAsync_ShouldReturnTrue()
        {
            //Arrange
            var users = new List<User>
            {
               new User
               {
                   Id = "1",
               }
            };
            _userManager.GetUsersInRoleAsync(RolesConstants.ADMIN).Returns(Task.FromResult<IList<User>>(users));

            //Act
            var response = await _accountRepository.IsAdminExistsAsync();

            //Assert
            response.Should().BeTrue();
        }

        [Fact]
        public async Task SignIn_ShouldReturnSignInResult()
        {
            //Arrange
            var request =
                new
                {
                    email = "Test@gmail.com",
                    password = "Test@gmail.com",
                    isPersistent = false,
                };
            var singIn = SignInResult.Success;
            _signInManager.PasswordSignInAsync(request.email, request.password, request.isPersistent, true).Returns(Task.FromResult(singIn));

            //Act
            var response = await _accountRepository.SignIn(request.email, request.password, request.isPersistent);

            //Assert
            response.Should().Be(singIn);
        }

        [Fact]
        public async Task IsMemberExist_ShouldReturnTrue()
        {
            //Arrange
            var memberId = Guid.NewGuid();

            var member = new Member
            {
                Name = "Name",
                Sexe = "M",
                Id = memberId
            };

            _iccPlannerContext.Members.AddRange(member);
            await _iccPlannerContext.SaveChangesAsync();

            //Act
            var result = await _accountRepository.IsMemberExist(memberId.ToString());

            //Assert
            result.Should().BeTrue();
        }

       /* [Fact]  
        public async Task FindMemberByUserId_ShouldReturnMember()
        {
            //Arrange
            var userId = Guid.NewGuid();

            var user = new User
            {
               Id = userId.ToString(), 
            };

            _iccPlannerContext.Users.AddRange(user);
            await _iccPlannerContext.SaveChangesAsync();

            //Act
            var result = await _accountRepository.FindMemberByUserId(userId.ToString());

            //Assert
            result.Should().BeNull();
        }*/

        [Fact]  
        public async Task GetAuthMember_ShouldReturnMember()
        {
            //Arrange
            var userId = Guid.NewGuid();

            var user = new User
            {
               Id = userId.ToString(), 
            };

            _iccPlannerContext.Users.AddRange(user);
            await _iccPlannerContext.SaveChangesAsync();

            //Act
            var result = await _accountRepository.GetAuthMemberAsync(userId);

            //Assert
            result.Should().BeNull();
        }
         

    }
}
