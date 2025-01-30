using Application.Interfaces.Repositories;  
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class RoleRepository<T> : IRoleRepository<Role>
    {
        private readonly RoleManager <Role> roleManager;

        public RoleRepository(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public Task insert(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
