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

        public TabServicePrgService(IBaseRepository<TabServicePrg> baseRepository, IMapper mapper, IPrgDateRepository prgDateRepository,
            IServiceRepository serviceRepository,
            ITabServicePrgRepository tabServicePrgRepository) : base(baseRepository, mapper)
        {
            _prgDateRepository = prgDateRepository;
            _tabServicePrgRepository = tabServicePrgRepository;
            _serviceRepository = serviceRepository;
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

        public async Task<GetDatesResponse> GetDates(Guid? userId, int? month = null, int? year = null)
        {
            var date = DateOnly.Parse($"{year ?? DateTime.Now.Year}-{month ?? DateTime.Now.Month}-01");
            var dates = await _prgDateRepository.GetPrgDates(userId ?? Guid.Empty, date);
            return _mapper.Map<GetDatesResponse>(dates);
        }

        public async Task<Result<List<GetServicesListResponse>>> GetPrgServices(ServicesRequest request)
        {
            var rsl = await _tabServicePrgRepository.GetServicesAsync(request);
            return Result<List<GetServicesListResponse>>.Success(rsl);
        }

        public async Task<Result<List<GetServiceByDepart>>> GetServicePrgByDepartAsync(int idDepart, int month, int year)
        {
            var rsl = await _tabServicePrgRepository.GetServicePrgByDepart(idDepart, month, year);
            return Result<List<GetServiceByDepart>>.Success(rsl);
        }
    }
}
