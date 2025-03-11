// Ignore Spelling: Auth 
using Application.Helper;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Department;
using Application.Responses.Department;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services
{
    /// <summary>
    /// Ce service permet de gérer les action d'un Département 
    /// </summary>
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IDepartmentProgramRepository _departmentProgramRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IPostRepository postRepository, IAccountRepository accountRepository, IDepartmentProgramRepository departmentProgramRepository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
            _accountRepository = accountRepository;
            _departmentProgramRepository = departmentProgramRepository;
        }
        public async Task<AddDepartmentResponse> AddDepartment(AddDepartmentRequest addDepartmentRequest)
        {
            var departemtDto = _mapper.Map<Department>(addDepartmentRequest);
            var newDepartment = await _departmentRepository.Insert(departemtDto);

            return _mapper.Map<AddDepartmentResponse>(newDepartment);
        }

        public async Task AddDepartmentResponsable(AddDepartmentRespoRequest addDepartmentRespoRequest)
        {
            var memberDepartment = await _departmentRepository.FindDepartmentMember(addDepartmentRespoRequest.MemberId, addDepartmentRespoRequest.DepartmentId);

            //Verifier s'il est deja memberDepartment
            if (memberDepartment == null)
            {
                var departmentMemberDto = _mapper.Map<DepartmentMember>(addDepartmentRespoRequest);
                memberDepartment = await _departmentRepository.SaveDepartmentMember(departmentMemberDto);
            }

            var post = await _postRepository.FindPosteByName(MemberPost.RESPONSABLE_REFERENT);

            var departmentMemberPost = _mapper.Map<DepartmentMemberPost>(addDepartmentRespoRequest);
            departmentMemberPost.PosteId = post!.Id;
            departmentMemberPost.DepartmentMemberId = memberDepartment.Id;

            await _departmentRepository.SaveDepartmentMemberPost(departmentMemberPost);
        }

        public async Task AddDepartmentsProgram(AddDepartmentProgramRequest departmentProgramRequest, Guid? userAuthId)
        {
            var createdBy = await _accountRepository.GetAuthMember(userAuthId);
            var newDepartmentPrograms = await InitializeDepartmentProgramModel(departmentProgramRequest, createdBy);

            await _departmentProgramRepository.BulkInsertOptimizedAsync(newDepartmentPrograms);
        }

        public async Task<bool> IsNameExists(string name)
        {
            return await _departmentRepository.IsNameExistsAsync(name);
        }

        /// <summary>
        /// Initialise le modèle de programme de département à partir de <see cref="AddDepartmentProgramRequest"/> 
        /// pour preparer l'enregistrement en masse
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Liste de <see cref="DepartmentProgram"/></returns>
        public virtual async Task<IEnumerable<DepartmentProgram>> InitializeDepartmentProgramModel(AddDepartmentProgramRequest request, Member? CreatedBy)
        {
            var departmentIds = request.DepartmentIds.Split(",");
            var endDate = request.EndAt ?? request.StartAt;

            var departmentPrograms = departmentIds.SelectMany(departmetId =>
                Enumerable.Range(0, (endDate.DayNumber - request.StartAt.DayNumber + 1))
                           .Select(offset => new DepartmentProgram
                           {
                               DepartmentId = Int32.Parse(departmetId),
                               ProgramId = request.ProgramId,
                               StartAt = request.StartAt.AddDays(offset),
                               Localisation = request.Localisation,
                               Comment = request.Comment,
                               CreateById = CreatedBy!.Id
                           }));

            return await GetNonExistingProgramsAsync(departmentPrograms);
        }

        /// <summary>
        /// Permet d’éviter les doublons lors de l’insertion de la DB
        /// </summary>
        /// <param name="departmentPrograms"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DepartmentProgram>> GetNonExistingProgramsAsync(IEnumerable<DepartmentProgram> departmentPrograms)
        {
            var existingPrograms = await _departmentProgramRepository.GetExistingProgramDepartmentsAsync(departmentPrograms);

            if (!existingPrograms.Any())
            {
                return departmentPrograms;
            }

            var existingProgramsSet = new HashSet<(int ProgramId, int DepartmentId, DateOnly StartAt)>(
                existingPrograms.Select(ep => (ep.ProgramId, ep.DepartmentId, ep.StartAt))
            );

            return departmentPrograms
                .Where(dp => !existingProgramsSet.Any(existingProgram =>
                    existingProgram.ProgramId == dp.ProgramId &&
                    existingProgram.DepartmentId == dp.DepartmentId &&
                    existingProgram.StartAt == dp.StartAt))
                .ToList();
        }

        public async Task DeleteDepartmentProgramByIdsAsync(DeleteDepartmentProgramRequest deleteDepartmentProgramRequest)
        {
            //convertir la chêne de caractère en tableau de int(ids)
            var ids = Utiles.ConvertStringToArray(deleteDepartmentProgramRequest.DepartmentProgramIds)
                .Where(id => id.HasValue)
                .Select(id => id!.Value) // Convertir int? en int
                .ToList(); 

            await _departmentProgramRepository.BulkDeleteByIdsAsync(ids);
        }
    }
}
