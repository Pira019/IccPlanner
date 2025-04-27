using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Program;
using Application.Responses.Program;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class ProgramService : IProgramService
    {
        private readonly IProgramRepository _IProgramRepository;
        private readonly IMapper _mapper;
    
        public ProgramService(IProgramRepository iProgramRepository, IMapper mapper)
        {
            _IProgramRepository = iProgramRepository;
            _mapper = mapper;
        }
        public async Task<AddProgramResponse> Add(AddProgramRequest request)
        {
            var dtoProgram = _mapper.Map<Program>(request);
            var newProgram = await _IProgramRepository.Insert(dtoProgram);

            return _mapper.Map<AddProgramResponse>(newProgram);
        }

        public async Task<bool> IsNameExists(string programName)
        {
            return await _IProgramRepository.IsNameExistsAsync(programName);
        }
    }
}
