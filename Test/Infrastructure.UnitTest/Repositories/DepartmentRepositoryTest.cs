using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories; 
using Microsoft.EntityFrameworkCore;

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

        [Fact]
        public async Task FindDepartmentMember_ShouldReturnDepartmentMember()
        {
            //Arrange
            var memberId = Guid.NewGuid().ToString();
            var departmentId = 1;

            var departmentMember = new DepartmentMember
            {
                DepartementId = departmentId,
                MemberId = Guid.Parse(memberId)
            };

            _iccPlannerContext.DepartmentMembers.AddRange(departmentMember);
            await _iccPlannerContext.SaveChangesAsync();

            //Act
            var result = await _departmentRepository.FindDepartmentMember(memberId, departmentId);

            //Assert
            result.Should().NotBeNull();
        } 
        
        [Fact]
        public async Task SaveDepartmentMember_ShouldReturnDepartmentMember()
        {
            //Arrange 
            var departmentMember = new DepartmentMember
            {
                DepartementId = 3,
                MemberId = Guid.NewGuid()
            }; 
            //Act
            var result = await _departmentRepository.SaveDepartmentMember(departmentMember);

            //Assert
            result.Should().NotBeNull();
        } 
        
        [Fact]
        public async Task SaveDepartmentMemberPost_ShouldReturnTask()
        {
            //Arrange 
            var departmentMemberPost = new DepartmentMemberPost
            {
                Id = 1, 
                DepartmentMemberId = 1,
                PosteId = 1,
            };
            //Act
             await _departmentRepository.SaveDepartmentMemberPost(departmentMemberPost); 
        }

        [Fact]
        public async Task IsDepartmentIdExists_ShouldReturnBool()
        {
            //Arrange
            _iccPlannerContext.Departments.AddRange(
                new Department
                {
                    Description = "Desc",
                    Name = "Name",
                    Id = 1
                }
                );
            await _iccPlannerContext.SaveChangesAsync();

            //Act
            var result = await _departmentRepository.IsDepartmentIdExists(1);

            //Assert
            result.Should().BeTrue();
        }
    }
}
