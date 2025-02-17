using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Test.Infrastructure.UnitTest.Repositories
{
    public class BaseRepositoryTest
    {
        private readonly IccPlannerContext _iccPlannerContext;
        private readonly BaseRepository<User> _baseRepository;
        public BaseRepositoryTest()
        {
            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                   .UseInMemoryDatabase("fakeDb")
                   .Options;
            _iccPlannerContext = new IccPlannerContext(option);

            _baseRepository = new BaseRepository<User>(_iccPlannerContext);

        }

        [Fact]
        public async Task Insert_ShouldInsertEntity()
        {
            // Arrange 
            var entity = new User
            {
                Email = "Test@gmail.com"
            };

            await _iccPlannerContext.Set<User>().AddAsync(entity); 

            // Act
            var result = await _baseRepository.Insert(entity);

            // Assert 
            result.Should().Be(entity); 
        }
    }
}
