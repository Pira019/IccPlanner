using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.ServiceTab;
using Application.Responses;
using Application.Responses.Errors;
using Application.Responses.TabService;
using AutoMapper;
using Domain.Entities;
using Shared.Ressources;

namespace Application.Services
{
    /// <summary>
    ///     Permet de gérer les action d'un service
    /// </summary>
    public class ServiceTabService : BaseService<TabServices>, IServiceTabService
    {
        private IServiceRepository _serviceRepository;

        /// <summary>
        ///     Services par défaut (cultes typiques).
        /// </summary>
        private static readonly List<(string DisplayName, string Start, string End, string? Arrival)> DefaultServices =
        [
            ("Culte", "09:30", "11:00", null),
        ];

        public ServiceTabService(IBaseRepository<TabServices> baseRepository, IMapper mapper, IServiceRepository repository) : base(baseRepository, mapper)
        {
            _serviceRepository = repository;
        }
         
        public async Task<Result<int>> Add(AddServiceRequest request)
        {
            var start = TimeOnly.Parse(request.StartTime);
            var end = TimeOnly.Parse(request.EndTime);

            var existService = await _serviceRepository.IsServiceExist(start, end, request.DisplayName);

            if (existService)
            {
                return Result<int>.Fail(string.Format(ValidationMessages.SERVICE_EXISTS,request.DisplayName, request.StartTime, request.EndTime ));
            }
             
            var mapService = _mapper.Map<TabServices>(request);

            var rsl = await _serviceRepository.Insert(mapService);

            return Result<int>.Success(mapService.Id);  
        }

        public async  Task<bool> IsServiceExist(string startTime, string endTime, string displayServiceName)
        {
            var start = TimeOnly.Parse(startTime);
            var end = TimeOnly.Parse(endTime);
            return await _serviceRepository.IsServiceExist(start, end, displayServiceName);
        }

      /*  public override async Task<IEnumerable<object>> GetAll()
        {
            return _mapper.Map<List<GetTabServiceListResponse>>(await base.GetAll()); 
        } */

        /// <inheritdoc />
        public async Task SeedDefaultServicesAsync()
        {
            foreach (var (displayName, start, end, arrival) in DefaultServices)
            {
                var startTime = TimeOnly.Parse(start);
                var endTime = TimeOnly.Parse(end);

                var exists = await _serviceRepository.IsServiceExist(startTime, endTime, displayName);
                if (!exists)
                {
                    var service = new TabServices
                    {
                        DisplayName = displayName,
                        StartTime = startTime,
                        EndTime = endTime,
                        ArrivalTimeOfMember = arrival != null ? TimeOnly.Parse(arrival) : null
                    };
                    await _serviceRepository.Insert(service);
                }
            }
        }
    }
}
