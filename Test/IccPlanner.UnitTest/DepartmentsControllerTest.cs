using System.Security.Claims;
using Application.Helper;
using Application.Interfaces.Services;
using Application.Requests.Department; 
using Application.Responses.Errors.Department;
using Domain.Abstractions;
using FluentAssertions;
using IccPlanner.Controllers;
using Microsoft.AspNetCore.Http;
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
        public async Task Add_WhenSucceed_ShouldReturnCreated()
        {
            //Arrange
            var addRequest = new AddDepartmentRequest
            {
                Description = "Desc",
                MinistryId = 1,
                Name = "Name"
            };  

            //Act
            var result = await _departmentsController.Add(addRequest); 
            //Assert
            var createdObject = Assert.IsType<CreatedResult>(result);   
        }
        
        [Fact]
        public async Task AddResponsable_ShouldReturnOk()
        {
            //Arrange
            var addDepartmentRespoRequest = new AddDepartmentRespoRequest
            {
                DepartmentId = 1,
                MemberId = "123456789",
                StartAt = DateOnly.Parse("2025-02-02"),
                EndAt = DateOnly.Parse("2025-02-02"),
            }; 

            //Act
            var result = await _departmentsController.AddResponsable(addDepartmentRespoRequest); 
            
            //Assert
            var okResult = Assert.IsType<OkResult>(result);   
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK); 
        }

        [Fact]
        public async Task CreateDepartmentProgram_ShouldReturnOkResult() 
        {
            //Arrange 
            var addDepartmentProgramRequest = new AddDepartmentProgramRequest
            {
                DepartmentIds = "1,2",
                ProgramId = 1,
                StartAt = DateOnly.Parse("2024-05-20")
            };
            var mockUserId = Guid.NewGuid();
            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim (ClaimTypes.NameIdentifier, mockUserId.ToString() )
            }, "mock"));

            _departmentsController.ControllerContext = new ControllerContext 
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };

           _departmentService.AddDepartmentsProgram(addDepartmentProgramRequest, mockUserId).Returns(Task.CompletedTask);

            //Act
            var result = await _departmentsController.CreateDepartmentProgram(addDepartmentProgramRequest);

            var okResult = Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async Task DeleteDepartmentProgram_ShouldReturnNoContent()
        {
            //Arrange 
            var request = new DeleteDepartmentProgramRequest
            {
                DepartmentProgramIds = "1,2",
            };

            //Act
            var result = await _departmentsController.DeleteDepartmentProgram(request);

            var noContent = Assert.IsType<NoContentResult>(result);
            noContent.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        }
    }
}
