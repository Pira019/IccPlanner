﻿using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Permet de verifier si le nom de département existe deja 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<bool> IsNameExistsAsync(string name);

        /// <summary>
        /// Si le département existe par Id
        /// </summary>
        /// <param name="id"> Id de département </param>
        /// <returns></returns>
        public Task<bool> IsDepartmentIdExists(int id);

        /// <summary>
        /// Permet de trouver un membre d'un département
        /// </summary>
        /// <param name="memberId"> Id du membre</param>
        /// <param name="departmentId"> Id du département </param>
        /// <returns></returns>
        public Task<DepartmentMember?> FindDepartmentMember(string memberId, int departmentId);
        public Task<DepartmentMember> SaveDepartmentMember(DepartmentMember departmentMember);
        public Task SaveDepartmentMemberPost(DepartmentMemberPost departmentMemberPost);


    }
}
