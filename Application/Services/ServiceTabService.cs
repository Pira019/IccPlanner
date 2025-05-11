using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    /// <summary>
    /// Permet de gérer les action d'un service
    /// </summary>
    public class ServiceTabService : BaseService<TabServices>, IServiceTabService
    { 
        private IServiceRepository _serviceRepository;
        public ServiceTabService(IBaseRepository<TabServices> baseRepository, IMapper mapper, IServiceRepository repository) : base(baseRepository, mapper)
        {
            _serviceRepository = repository;
        }

        public override async Task<Object> Add(Object entity)
        { 
            var result = await base.Add(entity);
            return _mapper.Map<BaseAddResponse>(result);
        }

        public async Task<bool> IsServiceExist(string startTime, string endTime, string displayServiceName)
        {
            var start = TimeOnly.Parse(startTime);
            var end = TimeOnly.Parse(endTime);
            return await _serviceRepository.IsServiceExist(start, end, displayServiceName); 
        }
    }
}
