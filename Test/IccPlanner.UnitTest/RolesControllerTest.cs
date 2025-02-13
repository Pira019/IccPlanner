using Application.Dtos.Role;
using Application.Interfaces.Services;
using Application.Requests.Role;
using Application.Responses.Errors;
using Application.Responses.Role;
using FluentAssertions;
using IccPlanner.Controllers;  
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Test.IccPlanner.UnitTest
{
    /// <summary>
    /// Teste Roles 
    /// </summary>
    public class RolesControllerTest
    {
        private readonly IRoleService _roleService;
        private readonly RolesController _rolesController;

        public RolesControllerTest()
        {
            _roleService = Substitute.For<IRoleService>();
            _rolesController = new RolesController(_roleService);
        }

        [Fact]
        public async void GetRole_ShouldReturnRolesList()
        {
            //Arrange
            var data = new List<GetRolesDto>
            {
                new GetRolesDto { Id = "1", Description = "Desc", Name = "NomRole" }
            };
            _roleService.GetAll().Returns(Task.FromResult((ICollection<GetRolesDto>)data));

            //Act
            var request = await _rolesController.Get();

            //Assert
            var result = Assert.IsType<OkObjectResult>(request).Value;
            var response = new GetRolesResponse { Items = data };

            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async void CreateRole_WhenNotSucceed_ShouldReturnBadRequest()
        {
            //Arrange
            var identityResponse = IdentityResult.Failed();
            var createRoleRequest = new CreateRoleRequest
            {
                Name = "Test",
                Description = "Description"
            };

            _roleService.CreateRole(createRoleRequest).Returns(Task.FromResult(identityResponse));

            //Act
            var request = await _rolesController.CreateRole(createRoleRequest);

            //Assert
            var result = Assert.IsType<BadRequestObjectResult>(request).Value;
            result.Should().BeEquivalentTo(ApiError.ApiIdentityResultResponseError(identityResponse));
        }

        [Fact]
        public async void CreateRole_WhenSucceed201_ShouldReturnCreatedResult()
        {
            //Arrange
            var identityResponse = IdentityResult.Success;
            var createRoleRequest = new CreateRoleRequest
            {
                Name = "Test",
                Description = "Description"
            };

            //
            var createResponse = new CreateRoleResponse
            {
                Id = "test"
            };

            _roleService.CreateRole(createRoleRequest).Returns(Task.FromResult(identityResponse));
            _roleService.GetRoleByName(Arg.Any<string>()).Returns(Task.FromResult<CreateRoleResponse>(createResponse));

            //Act
            var request = await _rolesController.CreateRole(createRoleRequest);

            //Assert
            var result = Assert.IsType<CreatedResult>(request).Value;
            result.Should().Be(result);
        }  
    }
}
