using Application.Dtos.Department;
using Application.Requests.Department;
using AutoMapper;
using Domain.Entities;

namespace Test.Application.UnitTest.Dtos.DepartmentTest
{
    public class DepartmentMappingProfileTest
    {
        private readonly IMapper _mapper;

        public DepartmentMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DepartmentMappingProfile>();
            });

            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_AddDepartmentRequest_To_Department()
        {
            var request = new AddDepartmentRequest
            {
                // Simule des valeurs d'entrée
                Name = "IT AddDepartmentProgramRequestValidation",
                Description = "IT-01",
                MinistryId = 1,
            };

            var result = _mapper.Map<Department>(request);

            Assert.NotNull(result);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Description, result.Description);
        }

       /* [Fact]
         public void Should_Map_AddDepartmentProgramRequest_To_Department()
        {
            var request = new AddDepartmentProgramRequest
            {
                // Simule des valeurs d'entrée 
                DepartmentIds = "1,2,3",
                ProgramId = 1,
                StartAt = DateOnly.Parse("2024-01-25"),
                EndAt = DateOnly.Parse("2024-01-25"),
                Comment = "test",
                Localisation = "test", 
            };

            var result = _mapper.Map<DepartmentProgram>(request);

            Assert.NotNull(result); ;
        }

        [Fact]
        public void Should_Map_AddDepartmentRespoRequest_To_DepartmentMember()
        {
            var request = new AddDepartmentRespoRequest
            {
                DepartmentId = 1,
                MemberId = Guid.NewGuid().ToString(),
            };

            var result = _mapper.Map<DepartmentMember>(request);

            Assert.NotNull(result);
            Assert.Equal(request.DepartmentId, result.DepartmentId);
        }

        [Fact]
        public void Should_Map_AddDepartmentRespoRequest_To_DepartmentMemberPost_With_Ignored_Fields()
        {
            var request = new AddDepartmentRespoRequest
            {
                DepartmentId = 1,
                MemberId = Guid.NewGuid().ToString(),
                StartAt = DateOnly.Parse("2024-01-25"),
                EndAt = DateOnly.Parse("2024-01-25"),
            };

            var result = _mapper.Map<DepartmentMemberPost>(request);

            Assert.NotNull(result);
            Assert.Equal(0, result.PosteId); // Propriétés ignorées
            Assert.Equal(0, result.DepartmentMemberId);
        }*/

    }
}
