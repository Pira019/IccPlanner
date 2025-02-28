using Application.Interfaces.Services;
using Application.Requests.Program;
using Application.Responses.Program;
using FluentAssertions;
using IccPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Test.IccPlanner.UnitTest
{
    public class ProgramsControllerTest
    {
        private readonly IProgramService _programService;

        private readonly ProgramsController _programsController;

        public ProgramsControllerTest()
        {
            _programService = Substitute.For<IProgramService>();
            _programsController = new ProgramsController(_programService);
        }

        [Fact]
        public async Task CreateProgram_ShouldReturnCreated()
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

            _programService.Add(request).Returns(Task.FromResult(res));

            //Act
            var response = await _programsController.CreateProgram(request);
            
            //Assert
            var resultRespponse = Assert.IsType<CreatedResult>(response);

            resultRespponse.Value.Should().BeEquivalentTo(res);   
            Assert.NotNull(response);




        }
    }
}
