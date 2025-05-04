using Application.Dtos.Department; 
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Test.Infrastructure.UnitTest.Repositories
{
    public class DepartmentProgramRepositoryTest
    {
        private readonly IccPlannerContext _iccPlannerContext;
        private DepartmentProgramRepository _departmentProgramRepository;

        public DepartmentProgramRepositoryTest()
        {
            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                  .UseInMemoryDatabase("fakeDb")
            .Options;
            _iccPlannerContext = new IccPlannerContext(option);

            _departmentProgramRepository = new DepartmentProgramRepository(_iccPlannerContext);
        } 
    }
}
