using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Test.Infrastructure.UnitTest.Repositories
{

    public class ProgramRepositoryTest
    {
        private readonly IccPlannerContext _iccPlannerContext;

        private readonly ProgramRepository _ProgramRepository;

        public ProgramRepositoryTest()
        {
            var roleStore = Substitute.For<IRoleStore<Role>>();
            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                   .UseInMemoryDatabase("fakeDb")
                   .Options;

            _iccPlannerContext = new IccPlannerContext(option);

            _ProgramRepository = new ProgramRepository(_iccPlannerContext);

        }
    }
}
