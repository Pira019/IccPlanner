using Application.Dtos.Department; 
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Test.Infrastructure.UnitTest.Repositories
{
    public class DepartmentProgramRepositoryTest
    {
        private readonly IccPlannerContext _iccPlannerContext;
        private DepartmentProgramRepository _departmentProgramRepository;

        public DepartmentProgramRepositoryTest()
        {
            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                  .UseInMemoryDatabase("fakeDb")
            .Options;
            _iccPlannerContext = new IccPlannerContext(option);

            _departmentProgramRepository = new DepartmentProgramRepository(_iccPlannerContext);
        }

        [Fact]
        public async Task GetExistingProgramDepartmentsAsync()
        {
            // Arrange
            var departmentPrograms = new List<DepartmentProgram>
            {
                new DepartmentProgram
                {
                    ProgramId = 1,
                    DepartmentId = 1,
                    StartAt = DateOnly.Parse("2025-04-20"),
                    CreateById = Guid.NewGuid(),
                }
            };

            var existingDepartmentPrograms = new List<DepartmentProgramExistingDto>
            {
                new DepartmentProgramExistingDto
                {
                    ProgramId = 1,
                    DepartmentId = 1,
                    StartAt = DateOnly.Parse("2025-04-20")
                }
            };

            //Act
            var result = await _departmentProgramRepository.GetExistingProgramDepartmentsAsync(departmentPrograms);

            //Assert
            var dto = result.OfType<DepartmentProgramExistingDto>(); 
            result.Should().NotBeNull();    
        }
    }
}
