using Application.Interfaces.Services;
using Application.Requests.Ministry;
using Application.Responses.Errors.Ministry;
using Domain.Abstractions;
using FluentAssertions;
using IccPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Test.IccPlanner.UnitTest
{
    public class MinistriesControllerTest
    {
        private readonly IMinistryService _ministryService;
        private readonly MinistriesController _ministriesController;

        public MinistriesControllerTest()
        {
            _ministryService = Substitute.For<IMinistryService>();
            _ministriesController = new MinistriesController(_ministryService);
        }

        [Fact]
        public async Task Add_WhenMinistryNotExists_ShouldReturnCreated()
        {
            //Arrange
            var requestData = new AddMinistryRequest
            { Description = "description", Name = "Tes001t" };

            _ministryService.IsNameMinistryExists(Arg.Any<string>()).Returns(Task.FromResult(false));
            //Act
            var result = await _ministriesController.Add(requestData);
            //Assert
            Assert.IsType<CreatedResult>(result);

        } 
        
        [Fact]
        public async Task Add_WhenMinistryExists_ShouldReturnBadRequest()
        {
            //Arrange
            var requestData = new AddMinistryRequest
            { Description = "description", Name = "Tes001t" };

            _ministryService.IsNameMinistryExists(Arg.Any<string>()).Returns(Task.FromResult(true));
            //Act
            var result = await _ministriesController.Add(requestData);
            //Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            badRequest.Value.Should().BeEquivalentTo(MinistryResponseError.NameExist());


        }



    }
}
