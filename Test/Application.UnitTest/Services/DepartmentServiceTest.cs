using System;
using Application.Dtos.Department;
using Application.Helper;
using Application.Interfaces.Repositories;
using Application.Requests.Department;
using Application.Responses.Department;
using Application.Services;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace Test.Application.UnitTest.Services
{
    public class DepartmentServiceTest
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IDepartmentProgramRepository _departmentProgramRepository; 

        private readonly DepartmentService _departmentService;

        public DepartmentServiceTest()
        {
            _departmentRepository = Substitute.For<IDepartmentRepository>();
            _mapper = Substitute.For<IMapper>();
            _postRepository = Substitute.For<IPostRepository>();
            _accountRepository = Substitute.For<IAccountRepository>();
            _departmentProgramRepository = Substitute.For<IDepartmentProgramRepository>(); 

            _departmentService = new DepartmentService(_departmentRepository, _mapper, _postRepository, _accountRepository, _departmentProgramRepository);
        }

        [Fact]
        public async Task AddDepartment_ShouldReturnAddDepartmentResponse()
        {
            //Arrange
            var addRequest = new AddDepartmentRequest
            {
                Name = "name",
                Description = "description",
                MinistryId = 1,
                ShortName = "short",
                StartDate = DateOnly.Parse("2024-02-11")
            };

            var department = new Department
            {
                Description = "description",
                Name = "name",
                Id = 1,
                MinistryId = 1,
                ShortName = "short",
                StartDate = DateOnly.Parse("2024-02-11")
            };

            var response = new AddDepartmentResponse
            {
                Id = "1"
            };

            _mapper.Map<Department>(addRequest).Returns(department);
            _departmentRepository.Insert(Arg.Any<Department>()).Returns(Task.FromResult(department));
            _mapper.Map<AddDepartmentResponse>(department).Returns(response);

            //Act
            var result = await _departmentService.AddDepartment(addRequest);

            response.Should().Be(response);
            _mapper.Received(1).Map<Department>(addRequest);
        }

        [Fact]
        public async Task IsNameExists_ShouldReturnTrue()
        {
            //Arrange
            var name = "name";
            _departmentRepository.IsNameExistsAsync(name).Returns(true);

            //Act
            var result = await _departmentService.IsNameExists(name);

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public async Task AddDepartmentResponsable_WhenDepartmentIsNotFound_ShouldReturnTask()
        {
            //Arrange
            var addDepartmentRespoRequest = new AddDepartmentRespoRequest
            {
                DepartmentId = 1,
                MemberId = "123456789",
                StartAt = DateOnly.Parse("2025-02-02"),
                EndAt = DateOnly.Parse("2025-02-02"),
            };

            var departmentMember = new DepartmentMember
            {
                DepartmentId = 1,
                NickName = "123",
                MemberId = Guid.NewGuid()
            };

            var poste = new Poste
            {
                Description = "Desc",
                Name = "Name",
                Id = 1,
                ShortName = "123",
            };

            var departmentMemberPost = new DepartmentMemberPost
            {
                Id = 1,
                DepartmentMember = departmentMember,
                Poste = poste,
                DepartmentMemberId = 1,
            };

            _departmentRepository.FindDepartmentMember(addDepartmentRespoRequest.MemberId, addDepartmentRespoRequest.DepartmentId)
                .Returns(Task.FromResult<DepartmentMember?>(null));

            _mapper.Map<DepartmentMember>(addDepartmentRespoRequest).Returns(departmentMember);
            _departmentRepository.SaveDepartmentMember(departmentMember).Returns(Task.FromResult(departmentMember));

            _postRepository.FindPosteByName(MemberPost.RESPONSABLE_REFERENT).Returns(Task.FromResult(poste ?? null));

            _mapper.Map<DepartmentMemberPost>(addDepartmentRespoRequest).Returns(departmentMemberPost);

            //Act
            await _departmentService.AddDepartmentResponsable(addDepartmentRespoRequest);

            //Assert 
            await _departmentRepository.Received(1).SaveDepartmentMemberPost(Arg.Any<DepartmentMemberPost>());
        }

        [Fact]
        public async Task AddDepartmentResponsable_WhenDepartmentIsFound_ShouldReturnTask()
        {
            //Arrange
            var addDepartmentRespoRequest = new AddDepartmentRespoRequest
            {
                DepartmentId = 1,
                MemberId = "123456789",
                StartAt = DateOnly.Parse("2025-02-02"),
                EndAt = DateOnly.Parse("2025-02-02"),
            };

            var departmentMember = new DepartmentMember
            {
                DepartmentId = 1,
                NickName = "123",
                DateEntry = DateOnly.Parse("2025-02-02")
            };

            var poste = new Poste
            {
                Description = "Desc",
                Name = "Name",
                Id = 1,
                ShortName = "123",
            };

            var departmentMemberPost = new DepartmentMemberPost
            {
                Id = 1,
                DepartmentMember = departmentMember,
                Poste = poste,
                DepartmentMemberId = 1,
                StartAt = DateOnly.Parse("2025-02-02"),
                EndAt = DateOnly.Parse("2025-02-02")
            };

            _departmentRepository.FindDepartmentMember(addDepartmentRespoRequest.MemberId, addDepartmentRespoRequest.DepartmentId)
                .Returns(Task.FromResult<DepartmentMember?>(departmentMember));

            _postRepository.FindPosteByName(MemberPost.RESPONSABLE_REFERENT).Returns(Task.FromResult(poste ?? null));

            _mapper.Map<DepartmentMemberPost>(addDepartmentRespoRequest).Returns(departmentMemberPost);

            //Act
            await _departmentService.AddDepartmentResponsable(addDepartmentRespoRequest);

            //Assert 
            await _departmentRepository.Received(1).SaveDepartmentMemberPost(Arg.Any<DepartmentMemberPost>());
        }

       /* [Fact]
        public async Task AddDepartmentsProgram_ShouldReturnTask()
        {
            //Arrange
            var departmentProgramRequest = new AddDepartmentProgramRequest
            {
                DepartmentIds = "1",
                ProgramId = 1,
                StartAt = DateOnly.Parse("2025-04-20"),
                EndAt = DateOnly.Parse("2025-04-25"),
                Comment = "Comment",
                Localisation = "Ottawa"
            };
            var guid = Guid.NewGuid();
            var member = new Member
            {
                Id = guid,
                Name = "Name",
                Sexe = "M"
            };

            AccountRepository.GetAuthMemberAsync(guid).Returns(Task.FromResult((Member?)member));

            var departmentPrograms = new List<DepartmentProgram>
            {
                new DepartmentProgram
                {
                    CreateById = guid,
                    //Localisation = departmentProgramRequest.Localisation,
                    Comment = departmentProgramRequest.Comment,
                    DepartmentId = 1,
                    ProgramId = 1,
                    StartAt = departmentProgramRequest.StartAt


                }
            };
            //Act
            await _departmentService.AddDepartmentsProgram(departmentProgramRequest, guid);

        }*/

      /*  [Fact]
        public async Task AddDepartmentsProgram_WhenEndDateIsNull_ShouldReturnTask()
        {
            //Arrange
            var departmentProgramRequest = new AddDepartmentProgramRequest
            {
                DepartmentIds = "1",
                ProgramId = 1,
                StartAt = DateOnly.Parse("2025-04-20"),
                Comment = "Comment",
                Localisation = "Ottawa"
            };
            var guid = Guid.NewGuid();
            var member = new Member
            {
                Id = guid,
                Name = "Name",
                Sexe = "M"
            };

            AccountRepository.GetAuthMemberAsync(guid).Returns(Task.FromResult((Member?)member));

            var departmentPrograms = new List<DepartmentProgram>
            {
                new DepartmentProgram
                {
                    CreateById = guid,
                    //Localisation = departmentProgramRequest.Localisation,
                    Comment = departmentProgramRequest.Comment,
                    DepartmentId = 1,
                    ProgramId = 1,
                    StartAt = departmentProgramRequest.StartAt
                }
            };
            //Act
            await _departmentService.AddDepartmentsProgram(departmentProgramRequest, guid);

        }*/

       /* [Fact]
        public async Task GetNonExistingProgramsAsync_WhenDepartmentProgramExist_ShouldReturnFilteredDepartmentPrograms()
        {
            //Arrange 
            var departmentPrograms = new List<DepartmentProgram>
            {
                new DepartmentProgram
                {
                    CreateById = Guid.NewGuid(),
                    //Localisation = "departmentProgramRequest.Localisation",
                    Comment = "departmentProgramRequest.Comment",
                    DepartmentId = 1,
                    ProgramId = 1,
                    StartAt = DateOnly.Parse("2025-04-20")
                }
            };

            var departmentProgramDto = new List<DepartmentProgramExistingDto>
            {
                new DepartmentProgramExistingDto
                {

                    StartAt = DateOnly.Parse("2025-04-20"),
                    DepartmentId = 1,
                    ProgramId = 1, 
                }
            }; 

            _departmentProgramRepository.GetExistingProgramDepartmentsAsync(departmentPrograms).Returns(Task.FromResult(departmentProgramDto.AsEnumerable()));

            //Act
            var result = await _departmentService.GetNonExistingProgramsAsync(departmentPrograms);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetNonExistingProgramsAsync_WhenNoDepartmentProgramExist_ShouldReturnAllDepartmentPrograms()
        {
            // Arrange
            var departmentPrograms = new List<DepartmentProgram>
        {
            new DepartmentProgram
            {
                CreateById = Guid.NewGuid(),
                //Localisation = "Localisation1",
                Comment = "Comment1",
                DepartmentId = 1,
                ProgramId = 1,
                StartAt = DateOnly.Parse("2025-04-20")
            }
        };

            var departmentProgramDto = new List<DepartmentProgramExistingDto>(); // Aucun programme existant

            // Simuler le retour du repository
            _departmentProgramRepository.GetExistingProgramDepartmentsAsync(Arg.Any<IEnumerable<DepartmentProgram>>())
                .Returns(Task.FromResult(departmentProgramDto.AsEnumerable()));

            // Act
            var result = await _departmentService.GetNonExistingProgramsAsync(departmentPrograms);

            // Assert
            result.Should().NotBeNull();  
            result.Should().HaveCount(1);   
        }*/

        [Fact]
        public async Task DeleteDepartmentProgramByIdsAsync_ShouldReturnTask()
        {
            //Arrange
            var ids = new List<int?> { 1, 2 };
            var request = new DeleteDepartmentProgramRequest
            {
                DepartmentProgramIds = "1,2"
            };
            //Act
            await _departmentService.DeleteDepartmentProgramByIdsAsync(request);

            //Assert
          // await _departmentProgramRepository.Received().BulkDeleteByIdsAsync(ids);
        }
    }
}
