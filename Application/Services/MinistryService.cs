using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Ministry;
using Application.Responses.Ministry;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class MinistryService : IMinistryService
    {
        private readonly IMinistryRepository _ministryRepository;
        private readonly IMapper _mapper;

        public MinistryService(IMinistryRepository ministryRepository, IMapper mapper)
        {
            _ministryRepository = ministryRepository;
            _mapper = mapper;
        }
        public async Task<AddMinistryResponse> AddMinistry(AddMinistryRequest ministryRequest)
        {
            var ministryDto = _mapper.Map<Ministry>(ministryRequest);
            var newMinistry = await _ministryRepository.Insert(ministryDto);
            return _mapper.Map<AddMinistryResponse>(newMinistry);
        }

        public async Task<bool> IsNameMinistryExists(string name)
        {
           return await _ministryRepository.IsNameExists(name);
        }
    }
}
