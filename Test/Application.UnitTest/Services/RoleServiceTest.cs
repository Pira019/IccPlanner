using Application.Dtos.Role;
using Application.Interfaces.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace Test.Application.UnitTest.Services
{
    public class RoleServiceTest
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        private RoleService _roleService;

        public RoleServiceTest()
        {
            _roleRepository = Substitute.For<IRoleRepository>();
            _mapper = Substitute.For<IMapper>();

            _roleService = new RoleService(_roleRepository, _mapper);
        }

        [Fact]
        public async void GetAll_ShouldReturnNotNull()
        {
            //Arrange
            var roles = new List<Role>
            {
                new Role { Description = "Test", Name = "Test", Id = "122345"},
            };

            _roleRepository.GetAllRoles().Returns(Task.FromResult<List<Role>>(roles));

            var expectedResponse = new List<GetRolesDto>
            {
                new GetRolesDto { Description = "Test", Name = "Test", Id = "122345" }
            };

            //Act
            var response = await _roleService.GetAll();

            //Assert
            response.Should().NotBeNull(); 
            await _roleRepository.Received(1).GetAllRoles();
        }
    }
}
