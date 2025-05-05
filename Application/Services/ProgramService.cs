using Application.Dtos;
using Application.Dtos.Program;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Program;
using Application.Responses.Program;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Utiles;

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

        public async Task<PaginatedDto<ProgramDto>> GetPaginatedProgram(int pageIndex, int pageSize)
        {
            var query = _IProgramRepository.QueryAll();

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var item = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProgramDto
                {
                    Id = p.Id,  
                    Name = p.Name,
                    NbrDepartment = p.ProgramDepartments.Count()
                })
                .ToListAsync();

            return new PaginatedDto<ProgramDto>
            {
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Items = item
            };
        }
    }
}
