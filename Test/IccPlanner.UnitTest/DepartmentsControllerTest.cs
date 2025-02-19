using Application.Interfaces.Services;
using Application.Requests.Department;
using Application.Responses.Department;
using Application.Responses.Errors.Department;
using Domain.Abstractions;
using FluentAssertions;
using IccPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Test.IccPlanner.UnitTest
{
    public class DepartmentsControllerTest
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMinistryService _ministryService;

        private readonly DepartmentsController _departmentsController;

        public DepartmentsControllerTest()
        {
            _departmentService = Substitute.For<IDepartmentService>();
            _ministryService = Substitute.For<IMinistryService>();

            _departmentsController = new DepartmentsController(_departmentService, _ministryService);
        }

        [Fact]
        public async Task Add_WhenMinistryNotExist_ShouldReturnBadRequest()
        {
            //Arrange
            var addRequest = new AddDepartmentRequest
            {
                Description = "Desc",
                MinistryId = 1,
                Name = "Name"
            };

            _ministryService.IsMinistryExistsById(addRequest.MinistryId).Returns(Task.FromResult(false));

            //Act
            var result = await _departmentsController.Add(addRequest);

            //Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            badRequest.Value.Should().BeEquivalentTo(DepartmentResponseError.ErrorMessage(MinistryError.NAME_NOT_FOUND));
        }

        [Fact]
        public async Task Add_WhenNameExist_ShouldReturnBadRequest()
        {
            //Arrange
            var addRequest = new AddDepartmentRequest
            {
                Description = "Desc",
                MinistryId = 1,
                Name = "Name"
            };

            _ministryService.IsMinistryExistsById(addRequest.MinistryId).Returns(Task.FromResult(true));
            _departmentService.IsNameExists(Arg.Any<string>()).Returns(Task.FromResult(true));

            //Act
            var result = await _departmentsController.Add(addRequest);

            //Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            badRequest.Value.Should().BeEquivalentTo(DepartmentResponseError.ErrorMessage(DepartmentError.NAME_EXISTS));
        }

        [Fact]
        public async Task Add_WhenSucceed_ShouldReturnCreated()
        {
            //Arrange
            var addRequest = new AddDepartmentRequest
            {
                Description = "Desc",
                MinistryId = 1,
                Name = "Name"
            };

            _ministryService.IsMinistryExistsById(addRequest.MinistryId).Returns(Task.FromResult(true));
            _departmentService.IsNameExists(Arg.Any<string>()).Returns(Task.FromResult(false)); 

            //Act
            var result = await _departmentsController.Add(addRequest); 
            //Assert
            var createdObject = Assert.IsType<CreatedResult>(result);   
        }
    }
}
