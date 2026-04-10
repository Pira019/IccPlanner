// Ignore Spelling: Prg

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.ServiceTab;
using Application.Responses.ServicePrg;
using Application.Responses.TabService;
using AutoMapper;
using Domain.Entities;
using Shared.Ressources;

namespace Application.Services
{
    public class TabServicePrgService : BaseService<TabServicePrg>, ITabServicePrgService
    {
        private IPrgDateRepository _prgDateRepository;
        private ITabServicePrgRepository _tabServicePrgRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IDepartmentMemberRepository _departmentMemberRepository;
        private readonly IClaimRepository _claimRepository;

        public TabServicePrgService(IBaseRepository<TabServicePrg> baseRepository, IMapper mapper, IPrgDateRepository prgDateRepository,
            IServiceRepository serviceRepository,
            IDepartmentMemberRepository departmentMemberRepository,
            IClaimRepository claimRepository,
            ITabServicePrgRepository tabServicePrgRepository) : base(baseRepository, mapper)
        {
            _prgDateRepository = prgDateRepository;
            _tabServicePrgRepository = tabServicePrgRepository;
            _serviceRepository = serviceRepository;
            _departmentMemberRepository = departmentMemberRepository;
            _claimRepository = claimRepository;
        }

        public async Task<Result<bool>> AddServicePrg(AddServicePrgDepartmentRequest prgDepartmentRequest)
        {
            // Vérification de l'existence du service
            var serviceExists = await _serviceRepository.GetByIdAsync(prgDepartmentRequest.ServiceId);

            if (serviceExists == null)
            {
                return Result<bool>.Fail(string.Format(ValidationMessages.NOT_EXIST, ValidationMessages.SERVICE_));
            }

            // Vérification de l'existence de la date du programme
            var prgDateExists = await _prgDateRepository.IsExistAsync(prgDepartmentRequest.PrgDateId);

            if (!prgDateExists)
            {
                return Result<bool>.Fail(string.Format(ValidationMessages.NOT_EXIST, ValidationMessages.PROGRAM_));
            }

            // verification de l'existence d'un programme de service pour la même date
            var  prgServiceExiste  = await  _tabServicePrgRepository.IsServicePrgExistAsync(prgDepartmentRequest.ServiceId, prgDepartmentRequest.PrgDateId);
            if (prgServiceExiste)
            {
                return Result<bool>.Fail(ValidationMessages.PGM_SERVICE_EXIST);
            }

            List<TabServicePrg> servicePrgs = new List<TabServicePrg>();

            var prgDateIds = await _prgDateRepository.GetRecurringPrgDateIdsFromNowAsync(prgDepartmentRequest.PrgDateId);

            var newService = new TabServicePrg
            {
                Notes = prgDepartmentRequest.Notes,
                ArrivalTimeOfMember = string.IsNullOrWhiteSpace(prgDepartmentRequest.MemberArrivalTime) ? serviceExists.ArrivalTimeOfMember : TimeOnly.Parse(prgDepartmentRequest.MemberArrivalTime),
                DisplayName = prgDepartmentRequest.DisplayName ?? serviceExists.DisplayName,
                TabServicesId = prgDepartmentRequest.ServiceId,
            };

            //Si le programme est de type reccuerrent, on ajoute un programme de service pour chaque date retournée par GetRecurringPrgDateIdsFromNowAsync, sinon on ajoute un seul programme de service avec la date spécifiée dans la requete
            if (prgDateIds.Any())
            {
                foreach (var item in prgDateIds.ToList())
                {
                    newService.PrgDateId = item;
                    servicePrgs.Add(newService);
                }
            }
            else
            {
                newService.PrgDateId = prgDepartmentRequest.PrgDateId;
                servicePrgs.Add(newService);
            }

            await _tabServicePrgRepository.InsertAllAsync(servicePrgs); 
            return Result<bool>.Success(true);
        } 

        public async Task<Result<List<GetServicesListResponse>>> GetPrgServices(ServicesRequest request)
        {
            var rsl = await _tabServicePrgRepository.GetServicesAsync(request);
            return Result<List<GetServicesListResponse>>.Success(rsl);
        }

        public async Task<Result<GetServiceByDepart?>> GetServicePrgByDepartAsync(int idDepart, DateOnly dateOnly, Guid? memberId)
        {
            var rsl = await _tabServicePrgRepository.GetServicePrgByDepart(idDepart, dateOnly, memberId);
            return Result<GetServiceByDepart?>.Success(rsl);
        }

        public async Task<Result<List<GetDatesResponse>>> GetDatesByDepartAsync(int month, int year, int IdDepart)
        {
            var rsl = await _prgDateRepository.GetPrgServiceDatesAsync(month, year, IdDepart);
            var response = rsl.Select(date => new GetDatesResponse { Date = date }).ToList();
            return Result<List<GetDatesResponse>>.Success(response);
        }

        public async Task<Result<bool>> DeleteServicePrgAsync(int servicePrgId, Guid actionById, string permissionName)
        {
            var service = await _tabServicePrgRepository.GetByIdAsync(servicePrgId);
            if (service == null)
            {
                return Result<bool>.Fail(string.Format(ValidationMessages.NOT_EXIST, ValidationMessages.SERVICE_));
            }

            // Vérifier les droits : IndGest sur le département concerné OU claim CanManagDepart
            var departmentId = await _tabServicePrgRepository.GetDepartmentIdByServicePrgId(servicePrgId);
            if (departmentId.HasValue)
            {
                var hasRight = await _departmentMemberRepository.HasManagementRightAsync(actionById, departmentId.Value)
                    || await _claimRepository.HasClaimAsync(actionById.ToString(), permissionName);
                if (!hasRight)
                {
                    return Result<bool>.Fail(ValidationMessages.FORBIDDEN_ACCESS);
                }
            }

            await _tabServicePrgRepository.DeleteAsync(servicePrgId);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateServicePrgAsync(int servicePrgId, UpdateServicePrgRequest request, Guid actionById, string permissionName)
        {
            var service = await _tabServicePrgRepository.GetByIdAsync(servicePrgId);
            if (service == null)
            {
                return Result<bool>.Fail(string.Format(ValidationMessages.NOT_EXIST, ValidationMessages.SERVICE_));
            }

            // Vérifier les droits : IndGest sur le département concerné OU claim CanManagDepart
           /* var departmentId = await _tabServicePrgRepository.GetDepartmentIdByServicePrgId(servicePrgId);
            if (departmentId.HasValue)
            {
                var hasRight = await _departmentMemberRepository.HasManagementRightAsync(actionById, departmentId.Value)
                    || await _claimRepository.HasClaimAsync(actionById.ToString(), permissionName);
                if (!hasRight)
                {
                    return Result<bool>.Fail(ValidationMessages.FORBIDDEN_ACCESS);
                }
            }*/

            // Changer le service de base si demandé
            if (request.TabServicesId.HasValue && request.TabServicesId.Value != service.TabServicesId)
            {
                var newService = await _serviceRepository.GetByIdAsync(request.TabServicesId.Value);
                if (newService == null)
                {
                    return Result<bool>.Fail(string.Format(ValidationMessages.NOT_EXIST, ValidationMessages.SERVICE_));
                }
                service.TabServicesId = request.TabServicesId.Value;
            }
            // Si les heures sont modifiées, chercher ou créer un TabServices correspondant
            else if (!string.IsNullOrWhiteSpace(request.StartTime) && !string.IsNullOrWhiteSpace(request.EndTime))
            {
                var startTime = TimeOnly.Parse(request.StartTime);
                var endTime = TimeOnly.Parse(request.EndTime);
                var displayName = request.DisplayName ?? service.DisplayName;

                // Chercher un service existant avec ces heures et ce nom
                var existingService = await _serviceRepository.FindByTimeAndNameAsync(startTime, endTime, displayName);

                if (existingService != null)
                {
                    service.TabServicesId = existingService.Id;
                }
                else
                {
                    // Créer un nouveau TabServices
                    var newTabService = new TabServices
                    {
                        StartTime = startTime,
                        EndTime = endTime,
                        DisplayName = displayName,
                        ArrivalTimeOfMember = !string.IsNullOrWhiteSpace(request.ArrivalTimeOfMember)
                            ? TimeOnly.Parse(request.ArrivalTimeOfMember) : null
                    };
                    var created = await _serviceRepository.Insert(newTabService);
                    service.TabServicesId = created.Id;
                }
            }

            if (!string.IsNullOrWhiteSpace(request.DisplayName))
            {
                service.DisplayName = request.DisplayName;
            }

            if (!string.IsNullOrWhiteSpace(request.ArrivalTimeOfMember))
            {
                service.ArrivalTimeOfMember = TimeOnly.Parse(request.ArrivalTimeOfMember);
            }

            service.Notes = request.Notes;

            await _tabServicePrgRepository.UpdateAsync(service);
            return Result<bool>.Success(true);
        }
    }
}
