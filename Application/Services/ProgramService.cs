using Application.Dtos;
using Application.Dtos.Program;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Program;
using Application.Responses.Program;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Ressources;

namespace Application.Services
{
    public class ProgramService : BaseService<Program>, IProgramService
    {
        private readonly IProgramRepository _IProgramRepository;
        private readonly IClaimRepository _ClaimRepository;
        private readonly IDepartmentMemberRepository _departmentMemberRepository;

        public ProgramService(IBaseRepository<Program> baseRepository, IMapper mapper,
            IProgramRepository programRepository, IClaimRepository claimRepository, IDepartmentMemberRepository departmentMemberRepository)
            : base(baseRepository, mapper)
        {
            _IProgramRepository = programRepository;
            _ClaimRepository = claimRepository;
            _departmentMemberRepository = departmentMemberRepository;
        }

        public async Task<Result<AddProgramResponse>> Add(AddProgramRequest request, string userAuth, string permissionName)
        {
            bool hasClaim = await _ClaimRepository.HasClaimAsync(userAuth, permissionName);
            bool canManage = !hasClaim && await _departmentMemberRepository.CanManageMembersAsync(userAuth);

            var isAutor = hasClaim || canManage;
            // Verifier si l'utilisateur a la permission nécessaire.
            if (!isAutor)
            {
                return Result<AddProgramResponse>.Fail(ValidationMessages.UNAUTHORIZED);
            }

            if (await _IProgramRepository.IsNameExistsAsync(request.Name))
            {
                return Result<AddProgramResponse>.Fail(string.Format(ValidationMessages.EXIST_Pro_Val, ValidationMessages.PROGRAM_NAME, request.Name));
            }

            var dtoProgram = _mapper.Map<Program>(request);
            var newProgram = await _IProgramRepository.Insert(dtoProgram);

            return Result<AddProgramResponse>.Success(_mapper.Map<AddProgramResponse>(newProgram));
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
