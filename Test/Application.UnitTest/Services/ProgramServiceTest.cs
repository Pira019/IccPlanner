using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Program;
using Application.Responses.Program;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace Test.Application.UnitTest.Services
{
    public class ProgramServiceTest
    {
        private readonly IProgramRepository _programRepository;
        private readonly IMapper _mapper;

        private readonly ProgramService _programService;

        public ProgramServiceTest()
        {
            _programRepository = Substitute.For<IProgramRepository>();
            _mapper = Substitute.For<IMapper>();

            _programService = new ProgramService(_programRepository, _mapper);
        }

        [Fact]
        
        public async Task Add_ShoulReturnAddResponse()
        {
            //Arrange 
            var request = new AddProgramRequest
            {
                Description = "Test",
                Name = "Test",
                ShortName = "Test",
            };

            var res = new AddProgramResponse
            {
                ProgramId = 1
            };

            var program = new Program
            {
                Name = "Test",
                Description = "Test",
                ShortName = null,
                Id = 1,
            };

            _mapper.Map<Program>(request).Returns(program);

            _programRepository.Insert(program).Returns(program);

            _mapper.Map<AddProgramResponse>(program).Returns(res);

            //Act 
            var response = await _programService.Add(request);
            
            response.Should().BeEquivalentTo(res);
            Assert.NotNull(response);

        }
    }
}
