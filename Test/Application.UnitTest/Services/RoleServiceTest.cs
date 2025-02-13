using Application.Dtos.Role;
using Application.Interfaces.Repositories;
using Application.Requests.Role;
using Application.Responses.Role;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace Test.Application.UnitTest.Services
{
    public class RoleServiceTest
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        private RoleService _roleService;

        public RoleServiceTest()
        {
            _roleRepository = Substitute.For<IRoleRepository>();
            _mapper = Substitute.For<IMapper>();

            _roleService = new RoleService(_roleRepository, _mapper);
        }

        [Fact]
        public async void GetAll_ShouldReturnNotNull()
        {
            //Arrange
            var roles = new List<Role>
            {
                new Role { Description = "Test", Name = "Test", Id = "122345"},
            };

            _roleRepository.GetAllRoles().Returns(Task.FromResult<List<Role>>(roles));

            var expectedResponse = new List<GetRolesDto>
            {
                new GetRolesDto { Description = "Test", Name = "Test", Id = "122345" }
            };

            //Act
            var response = await _roleService.GetAll();

            //Assert
            response.Should().NotBeNull();
            await _roleRepository.Received(1).GetAllRoles();
        }

        [Fact]
        public async void GetRoleByName_ShouldReturnCreateRoleResponse()
        {
            //Arrange 
            var role = new Role { Id = "IdTest", Description = "Desc", Name = "test" };
            var roleName = "test";
            _roleRepository.GetRoleByNameAsync(roleName).Returns(Task.FromResult<Role?>(role));

            var createResponse = new CreateRoleResponse
            {
                Id = role.Id
            };
            _mapper.Map<CreateRoleResponse>(role).Returns(createResponse);

            //Act
            var response = await _roleService.GetRoleByName(roleName);

            //Assert
            var result = Assert.IsType<CreateRoleResponse>(response);
            response.Should().BeEquivalentTo(createResponse); ;
        }

        [Fact]
        public async Task CreateRole_ShouldReturnIdentityResult()
        {
            //Arrange
            var identityResult = IdentityResult.Success;
            var createrequest = new CreateRoleRequest
            {
                Name = "Role",
                Description = "Test",
            };

            _roleRepository.CreateAsync(Arg.Any<Role>()).Returns(Task.FromResult(identityResult));
            //Act
            var result = await _roleService.CreateRole(createrequest);
            //Assert
            result.Should().NotBeNull();
            result.Should().Be(identityResult);
        }
    }

}
