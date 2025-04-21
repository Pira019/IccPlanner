using Application.Interfaces.Services;
using Application.Requests.Ministry;  
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
            //Act
            var result = await _ministriesController.Add(requestData);
            //Assert
            Assert.IsType<CreatedResult>(result);

        }    
    }
}
