using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Test.Infrastructure.UnitTest.Repositories
{
    public class DepartmentRepositoryTest
    {
        private readonly IccPlannerContext _iccPlannerContext;
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentRepositoryTest()
        {
            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                   .UseInMemoryDatabase("fakeDb")
                   .Options;

            _iccPlannerContext = new IccPlannerContext(option);

            _departmentRepository = new DepartmentRepository(_iccPlannerContext);
        }

        [Fact]
        public async Task IsNameExists_ShouldReturnTrue()
        {
            //Arrange
            _iccPlannerContext.Departments.AddRange(new Department { Description = "Desc", Name = "Name" });
            await _iccPlannerContext.SaveChangesAsync();
            //Act
            var result = await _departmentRepository.IsNameExistsAsync("Name");

            //Assert
            result.Should().BeTrue();
        }
    }
}
