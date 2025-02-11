using Application.Dtos.Role;
using Application.Interfaces.Services;
using Application.Responses.Errors;
using Application.Responses.Role;
using FluentAssertions;
using IccPlanner.Controllers;
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
        public async void GetRole_ShouldRetursRolesList()
        {
            //Arrage
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

    }
}
