// Ignore Spelling: Prg

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.ServiceTab;
using Application.Responses.TabService;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class TabServicePrgService : BaseService<TabServicePrg>, ITabServicePrgService
    {
        private IPrgDateRepository _prgDateRepository;
        private ITabServicePrgRepository _tabServicePrgRepository;
        public TabServicePrgService(IBaseRepository<TabServicePrg> baseRepository, IMapper mapper, IPrgDateRepository prgDateRepository,
            ITabServicePrgRepository tabServicePrgRepository) : base(baseRepository, mapper)
        {
            _prgDateRepository = prgDateRepository;
            _tabServicePrgRepository = tabServicePrgRepository;
        }

        public async Task AddServicePrg(AddServicePrgDepartmentRequest prgDepartmentRequest)
        {
            List<TabServicePrg> servicePrgs = new List<TabServicePrg>();

            var prgDateIds = await _prgDateRepository.GetRecurringPrgDateIdsFromNowAsync(prgDepartmentRequest.PrgDateId);

            //Si le programme est de type Repository
            if (prgDateIds.Any())
            {
                foreach (var item in prgDateIds.ToList())
                {
                    servicePrgs.Add(
                        new TabServicePrg
                        {
                            PrgDateId = item,
                            Notes = prgDepartmentRequest.Notes,
                            ArrivalTimeOfMember = TimeOnly.Parse(prgDepartmentRequest.MemberArrivalTime!),
                            DisplayName = prgDepartmentRequest.DisplayName!,
                            TabServicesId = prgDepartmentRequest.ServiceId,
                        });
                }
            }
            else
            {
                servicePrgs.Add(
                       new TabServicePrg
                       {
                           PrgDateId = prgDepartmentRequest.PrgDateId,
                           Notes = prgDepartmentRequest.Notes,
                           ArrivalTimeOfMember = TimeOnly.Parse(prgDepartmentRequest.MemberArrivalTime!),
                           DisplayName = prgDepartmentRequest.DisplayName!,
                           TabServicesId = prgDepartmentRequest.ServiceId,
                       });
            }

            await _tabServicePrgRepository.InsertAllAsync(servicePrgs);
        }

        public async Task<GetDatesResponse> GetDates(Guid? userId, int? month = null, int? year = null)
        {
            var date = DateOnly.Parse($"{year ?? DateTime.Now.Year}-{month ?? DateTime.Now.Month}-01");
            var dates = await _prgDateRepository.GetPrgDates(userId ?? Guid.Empty, date);
            return _mapper.Map<GetDatesResponse>(dates);
        }
    }
}
