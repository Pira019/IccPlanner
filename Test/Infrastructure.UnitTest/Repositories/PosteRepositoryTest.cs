using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Test.Infrastructure.UnitTest.Repositories
{
    public class PosteRepositoryTest
    {
        private readonly IccPlannerContext _iccPlannerContext;
        private readonly PosteRepository _posteRepository;

        public PosteRepositoryTest()
        {
            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                   .UseInMemoryDatabase("fakeDb")
                   .Options;

            _iccPlannerContext = new IccPlannerContext(option);

            _posteRepository = new PosteRepository(_iccPlannerContext);
        }

        [Fact]
        public async Task IsNameExists_ShouldReturnTrue()
        {
            //Arrange
            _iccPlannerContext.Postes
                .AddRange(new Poste
                {
                    Description = "Desc",
                    ShortName = "Name",
                    Name = "Name",
                    Id = 1
                });
            await _iccPlannerContext.SaveChangesAsync();
            //Act
            var result = await _posteRepository.FindPosteByName("Name");

            //Assert
            result.Should().NotBeNull();
        }
    }
}
