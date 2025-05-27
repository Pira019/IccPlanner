// Ignore Spelling: Auth 
using Application.Dtos.Department;
using Application.Helper;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Department;
using Application.Responses.Department;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using OfficeOpenXml;
using Shared.Enums;

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

        public async Task AddDepartmentsProgram(AddDepartmentProgramRequest departmentProgramRequest, Guid userAuthId)
        { 
           var newDepartmentPrograms = await InitializeDepartmentProgramModel(departmentProgramRequest, userAuthId); 
           await _departmentProgramRepository.InsertAllAsync(newDepartmentPrograms);
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
        /// <param name="createdByUserGuid"></param>
        /// <returns>Liste de <see cref="DepartmentProgram"/></returns>
        public async Task<IEnumerable<DepartmentProgram>> InitializeDepartmentProgramModel(AddDepartmentProgramRequest request, Guid createdByUserGuid)
        { 
            var member = await _accountRepository.FindMemberByUserId(createdByUserGuid.ToString());

            //
            if(request.TypePrg == ProgramType.Recurring.ToString())
            {
                // Un programme récurrent n'utilise pas de dates précises
                request.Date?.Clear();
            }
            else
            {
                // Un programme ponctuel n'utilise pas de jours récurrent
                request.Day = null;
            }

            var data = request.DepartmentIds.Distinct()
                .Select(departmentId => 
                    new DepartmentProgram
                        {
                            DepartmentId = departmentId,
                            ProgramId = request.ProgramId,
                            CreateById  = member!.Id,
                            Comment = request.Comment,
                            Type = request.TypePrg,
                            PrgDepartmentInfo = new PrgDepartmentInfo 
                            { 
                                Dates = request.Date?.Select(d=> DateOnly.ParseExact(d,"yyyy-MM-dd")).ToList(),
                                Days = request.Day,
                                PrgDate = request.Date?.Select(d => new PrgDate
                                {
                                    Date = DateOnly.ParseExact(d, "yyyy-MM-dd")
                                }).ToList() ?? []
                            }
                        }
                    )
                .ToList();
            return data;    
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

        public async Task<int> ImportMembersAsync(AddDepartmentMemberImportFileRequest addDepartmentMemberImportFileRequest, Guid? authenticatedUser)
        {
            var addedBy = await _accountRepository.GetAuthMember(authenticatedUser);
            var departementMembers = new List<User>();  
             
            using (var stream = new MemoryStream())
            {
                addDepartmentMemberImportFileRequest.formFile.CopyTo(stream);
                stream.Position = 0;

                using (var package = new ExcelPackage(stream))
                {
                    foreach (var worksheet in package.Workbook.Worksheets)
                    {
                        // Récupérer les en-têtes de colonnes et leurs index
                        var columnIndexes = new Dictionary<string, int>();
                        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                        {
                            var header = worksheet.Cells[1, col].Text.Trim();
                            if (!string.IsNullOrEmpty(header))
                            {
                                columnIndexes[header] = col;
                            }
                        }

                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var importData = new ImportMemberDto
                            {
                                Contact = worksheet.Cells[row, columnIndexes[ColumnExcelFileNames.CONTACT.ToString()]].Text.Trim(),
                                DepartmentId = addDepartmentMemberImportFileRequest.DepartmentId,
                                Nom = worksheet.Cells[row, columnIndexes[ColumnExcelFileNames.PRENOM.ToString()]].Text,
                                Sex = worksheet.Cells[row, columnIndexes[ColumnExcelFileNames.SEXE.ToString()]].Text.Substring(0,1).ToUpper(),
                                AddedBy = addedBy!.Id
                            };

                            departementMembers.Add(_mapper.Map<User>(importData));
                        }
                    }
                }                
            };
            //Enregistrer 
            return await _accountRepository.SaveImportedMembersDepartment(departementMembers); 
        }

        public async Task<bool> IsValidDepartmentIds(IEnumerable<int> departmentIds)
        {
            if (departmentIds == null)
            {
                return false;
            }
            var distinctDepartmentIds = departmentIds.Distinct().ToList();
            var existingIds = await _departmentRepository.GetValidDepartmentIds(distinctDepartmentIds);

            return distinctDepartmentIds.All(id => existingIds.Contains(id));
        }
    }
} 