using Application.Interfaces.Repositories;
using Application.Requests.Department;
using Application.Responses.Department;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace Test.Application.UnitTest.Services
{
    public class DepartmentServiceTest
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        private readonly DepartmentService _departmentService;

        public DepartmentServiceTest() 
        {
            _departmentRepository = Substitute.For<IDepartmentRepository>();
            _mapper = Substitute.For<IMapper>();

            _departmentService = new DepartmentService(_departmentRepository, _mapper);
        }

        [Fact]
        public async Task AddDepartment_ShouldReturnAddDepartmentResponse()
        {
            //Arrange
            var addRequest = new AddDepartmentRequest
            {
                Name = "name",
                Description = "description",
                MinistryId = 1,
                ShortName = "short",
                StartDate = DateOnly.Parse("2024-02-11")
            };

            var department = new Department
            {
                Description = "description",
                Name = "name",
                Id = 1,
                MinistryId = 1,
                ShortName = "short",
                StartDate = DateOnly.Parse("2024-02-11")
            };

            var response = new AddDepartmentResponse
            {
                Id = "1"
            };

            _mapper.Map<Department>(addRequest).Returns(department);
            _departmentRepository.Insert(Arg.Any<Department>()).Returns(Task.FromResult(department));
            _mapper.Map<AddDepartmentResponse>(department).Returns(response);

            //Act
            var result = await _departmentService.AddDepartment(addRequest);

            response.Should().Be(response);
            _mapper.Received(1).Map<Department>(addRequest);
        }

        [Fact]
        public async Task IsNameExists_ShouldReturnTrue()
        {
            //Arrange
            var name = "name";
            _departmentRepository.IsNameExistsAsync(name).Returns(true);
             
            //Act
            var result = await _departmentService.IsNameExists(name);

           //Assert
           result.Should().Be(true);
        }
    }
}
