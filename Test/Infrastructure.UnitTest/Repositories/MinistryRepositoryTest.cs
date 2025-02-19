using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Test.Infrastructure.UnitTest.Repositories
{
    public class MinistryRepositoryTest
    {
        private readonly IccPlannerContext _iccPlannerContext;

        private readonly MinistryRepository _ministryRepository;

        public MinistryRepositoryTest()
        {
            var roleStore = Substitute.For<IRoleStore<Role>>();
            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                   .UseInMemoryDatabase("fakeDb")
                   .Options;

            _iccPlannerContext = new IccPlannerContext(option);

            _ministryRepository = new MinistryRepository(_iccPlannerContext);

        }

        [Fact]
        public async Task IsNameExists_ShouldReturnBool()
        {
            //Arrange
            _iccPlannerContext.Ministries.AddRange(new Ministry { Description = "Desc", Name = "Name" });
            await _iccPlannerContext.SaveChangesAsync();
            //Act
            var result = await _ministryRepository.IsNameExists("Name");

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task IsExists_ShouldReturnTrue()
        {
            //Arrange
            _iccPlannerContext.Ministries.AddRange(
                new Ministry
                {
                    Id = 2,
                    Description = "Desc",
                    Name = "Name"
                });

            await _iccPlannerContext.SaveChangesAsync();
            //Act
            var result = await _ministryRepository.IsExists(2);

            //Assert
            result.Should().BeTrue();
        }

    }
}
