using Application.Interfaces.Repositories;
using Application.Requests.Ministry;
using Application.Responses.Ministry;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using NSubstitute; 

namespace Test.Application.UnitTest.Services
{
    public class MinistryServiceTest
    {
        private readonly IMinistryRepository _ministryRepository;
        private readonly IMapper _mapper;

        private readonly MinistryService _ministryService;

        public MinistryServiceTest()
        {
            _ministryRepository = Substitute.For<IMinistryRepository>();
            _mapper = Substitute.For<IMapper>();

            _ministryService = new MinistryService(_ministryRepository, _mapper);
        }

        [Fact]
        public async Task AddMinistry_ShouldReturnAddMinistryResponse()
        {
            //Arrange
            var ministryRequest = new AddMinistryRequest
            {
                Name = "name",
                Description = "description",
            };

            var ministryEntity = new Ministry
            {
                Name = "name",
                Description = "description",
                Id = 1
            };

            var response = new AddMinistryResponse
            {
                Id = "1"
            };

            _mapper.Map<Ministry>(ministryRequest).Returns(ministryEntity);
            _ministryRepository.Insert(Arg.Any<Ministry>()).Returns(Task.FromResult(ministryEntity));
            _mapper.Map<AddMinistryResponse>(ministryEntity).Returns(response);

            //Act
            var result = await _ministryService.AddMinistry(ministryRequest);

            //Assert
            Assert.NotNull(result);

            _mapper.Received(1).Map<Ministry>(ministryRequest);
            await _ministryRepository.Received(1).Insert(Arg.Any<Ministry>()); 
            _mapper.Received(1).Map<AddMinistryResponse>(ministryEntity);
        }

        [Fact]
        public async Task IsNameMinistryExists_ShouldReturnTrue()
        {
            //Arrange
            var nameMinistry = "name";
            _ministryRepository.IsNameExists(Arg.Any<string>()).Returns(Task.FromResult(true));
             

            //Act
            var result = await _ministryService.IsNameMinistryExists(nameMinistry);

            //Assert 
            result.Should().BeTrue();
        }

        [Fact]
        public async Task IsMinistryExistsById_ShouldReturnTrue()
        {
            //Arrange
            var id =10;
            _ministryRepository.IsExistAsync(id).Returns(Task.FromResult(false));

            //Act
            var result = await _ministryService.IsMinistryExistsById(id);
            //Assert 
            result.Should().BeFalse();
        }
    }
}
