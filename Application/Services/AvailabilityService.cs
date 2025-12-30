

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Availability;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    /// <summary>
    ///    Service pour gérer les disponibilités des utilisateurs.
    /// </summary>
    public class AvailabilityService : BaseService<Availability>, IAvailabilityService
    {
        private readonly IDepartmentMemberRepository _departmentMemberRepository;
        public AvailabilityService(IBaseRepository<Availability> baseRepository, IMapper mapper,
            IDepartmentMemberRepository departmentMemberRepository) : base(baseRepository, mapper)
        {
            _departmentMemberRepository = departmentMemberRepository;
        }

        public Task<TabServices> Add(AddAvailabilityRequest addAvailabilityRequest, int idDepartmentMember)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        /*  public Task<TabServices> Add(AddAvailabilityRequest addAvailabilityRequest, int idDepartmentMember)
          {
              var availabilityEntity = _mapper.Map<Availability>(addAvailabilityRequest);
              availabilityEntity.DepartmentMemberId = idDepartmentMember;
              return base.Add(availabilityEntity);
          }*/

        /// <inheritdoc />
        public async Task<int?> GetIdDepartmentMember(Guid authMemberGuid, int? idDepartment)
        {
            return await _departmentMemberRepository.GetMemberInDepartmentIdAsync(idDepartment, authMemberGuid);
        } 
    }
}
