

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Availability;
using Application.Responses.ServicePrg;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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
            IAvailabilityRepository availabilityRepository, IHttpContextAccessor httpContextAccessor) : base(baseRepository, mapper, httpContextAccessor)
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
         
        async Task<Result<bool>> IAvailabilityService.Add(AddAvailabilityRequest addAvailabilityRequest, int? idDepart)
        {
            var userId = GetCurrentUserId();
            if (!Guid.TryParse(userId, out Guid userId_))
            {
                return Result<bool>.Fail(ValidationMessages.MJ_UtiliNonAutor);
            }

            // Vérifier que tous les ServicePrg appartiennent au département
            var allBelong = await _tabServicePrgRepository.AllBelongToDepartmentAsync(addAvailabilityRequest.ServicePrgIds, (int)idDepart!);
            if (!allBelong)
            {
                return Result<bool>.Fail(ValidationMessages.ServicePrgNonExist);
            }

            var member = await _accountRepository.GetAuthMemberAsync(userId_);
            var departmentMemberId = await _departmentMemberRepository.GetMemberInDepartmentIdAsync(idDepart, member.Id);

            if (departmentMemberId == null)
            {
                return Result<bool>.Fail(ValidationMessages.MJ_UtiliNonAutor);
            } 
            // Récupérer les doublons existants en une seule requête
            var existingIds = await _availabilityRepository.GetExistingServicePrgIdsAsync((int)departmentMemberId, addAvailabilityRequest.ServicePrgIds);

            var listServicePrgIds = addAvailabilityRequest.ServicePrgIds
                .Where(id => !existingIds.Contains(id))
                .Select(servicePrgId => new Availability
                {
                    DepartmentMemberId = (int)departmentMemberId,
                    TabServicePrgId = servicePrgId,
                })
                .ToList();

            if (listServicePrgIds.Any())
            {
                await _availabilityRepository.InsertAllAsync(listServicePrgIds);
            }

            return Result<bool>.Success(true);
        }
         
    }
}
