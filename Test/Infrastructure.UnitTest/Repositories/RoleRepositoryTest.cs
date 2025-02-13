using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
namespace Test.Infrastructure.UnitTest.Repositories
{
    public class RoleRepositoryTest
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IccPlannerContext _iccPlannerContext;

        private readonly RoleRepository _roleRepository;

        public RoleRepositoryTest()
        {
            var roleStore = Substitute.For<IRoleStore<Role>>();
            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                   .UseInMemoryDatabase("fakeDb")
                   .Options;

            _iccPlannerContext = new IccPlannerContext(option);

            _roleManager = Substitute.For<RoleManager<Role>>(roleStore, null, null, null, null);

            _roleRepository = new RoleRepository(_roleManager, _iccPlannerContext);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnIdentityResult()
        {
            //Arrange
            var identitySucceed = IdentityResult.Success;
            var role = new Role { Description = "Test", Name = "Test", Id = "Id" };


            _roleManager.CreateAsync(role).Returns(Task.FromResult(identitySucceed));

            //Act
            var result = await _roleRepository.CreateAsync(role);

            //Assert
            result.Should().Be(identitySucceed);
        }

        [Fact]
        public async Task GetAllRoles_ShouldReturnRoleList()
        {
            //Arrange  
            _iccPlannerContext.Roles.AddRange(new Role { Description = "Desc", Name = "Name", Id = "Test" });
            await _iccPlannerContext.SaveChangesAsync();
            //Act
            var result = await _roleRepository.GetAllRoles();

            //Assert 
            result.Should().NotBeNull();
        }
    }
}
