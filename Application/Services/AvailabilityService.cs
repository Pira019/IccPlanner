

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Availability;
using Application.Responses.ServicePrg;
using AutoMapper;
using Domain.Entities;
using Shared.Ressources;

namespace Application.Services
{
    /// <summary>
    ///    Service pour gérer les disponibilités des utilisateurs.
    /// </summary>
    public class AvailabilityService : BaseService<Availability>, IAvailabilityService
    {
        private readonly ITabServicePrgRepository _tabServicePrgRepository;
        private readonly IAccountRepository _accountRepository;

        private readonly IDepartmentMemberRepository _departmentMemberRepository;
        private readonly IAvailabilityRepository _availabilityRepository;

        public AvailabilityService(IBaseRepository<Availability> baseRepository, IMapper mapper,
            IDepartmentMemberRepository departmentMemberRepository, ITabServicePrgRepository tabServicePrgRepository, IAccountRepository accountRepository,
            IAvailabilityRepository availabilityRepository) : base(baseRepository, mapper)
        {
            _departmentMemberRepository = departmentMemberRepository;
            _accountRepository = accountRepository;
            _tabServicePrgRepository = tabServicePrgRepository;
            _availabilityRepository  = availabilityRepository;

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

        async Task<Result<int>> IAvailabilityService.Add(AddAvailabilityRequest addAvailabilityRequest)
        {
            var departId = await _tabServicePrgRepository.GetDepartmentIdByServicePrgId(addAvailabilityRequest.ServicePrgId);

            if (departId == null)
            {
                return Result<int>.Fail(ValidationMessages.ServicePrgNonExist);
            }

            var userId = GetCurrentUserId();
            var checkDepartmentMemberId = null as int?;

            if (Guid.TryParse(userId, out Guid userId_))
            {
                var member = await _accountRepository.GetAuthMemberAsync(userId_);
                checkDepartmentMemberId = await _departmentMemberRepository.GetMemberInDepartmentIdAsync(departId, member.Id);
            }               

            if (checkDepartmentMemberId == null) {

                return Result<int>.Fail(ValidationMessages.MJ_UtiliNonAutor);
            }

            var availabilityEntity = new Availability 
            {   
                DepartmentMemberId =(int)checkDepartmentMemberId,
                TabServicePrgId = addAvailabilityRequest.ServicePrgId,
            };

            await _availabilityRepository.Insert (availabilityEntity);
            return Result<int>.Success(availabilityEntity.Id);
        }
    }
}
