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
        private readonly IClaimRepository _claimRepository;
        private readonly IPrgDepartmentInfoRepository _prgDepartmentInfoRepository;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IPostRepository postRepository,
            IAccountRepository accountRepository, IDepartmentProgramRepository departmentProgramRepository, IClaimRepository claimRepository,
            IPrgDepartmentInfoRepository prgDepartmentInfoRepository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
            _accountRepository = accountRepository;
            _departmentProgramRepository = departmentProgramRepository;
            _claimRepository = claimRepository;
            _prgDepartmentInfoRepository = prgDepartmentInfoRepository;
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
            var departmentPrograms = await InitializeDepartmentProgramModel(departmentProgramRequest, userAuthId);
            await _departmentProgramRepository.InsertAlDepartmentProgramlAsync(departmentPrograms); 
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
            var member = await _accountRepository.FindMemberByUserIdAsync(createdByUserGuid.ToString());

            var effectiveDates = request.IndRecurent ? null : request.Dates;
            var effectiveDays = request.IndRecurent ? request.Days : null;

            var data = request.DepartmentIds.Distinct()
                .Select(departmentId =>
                    new DepartmentProgram
                    {
                        DepartmentId = departmentId,
                        ProgramId = request.ProgramId,
                        CreateById = member!.Id,
                        Comment = request.Comment,
                        IndRecurent = request.IndRecurent,
                        PrgDepartmentInfo = new PrgDepartmentInfo
                        {
                            PrgDate = effectiveDates?.Select(d => new PrgDate
                            {
                                Date = DateOnly.ParseExact(d, "yyyy-MM-dd")
                            }).ToList() ?? [],

                            PrgRecDays = effectiveDays?.Select(d => new PrgRecDay
                            {
                                Day = d
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
            var addedBy = await _accountRepository.GetAuthMemberAsync(authenticatedUser);
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
                                Sex = worksheet.Cells[row, columnIndexes[ColumnExcelFileNames.SEXE.ToString()]].Text.Substring(0, 1).ToUpper(),
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

        public async Task<GetDepartResponse> GetAsync(string userAuthId, List<string?> claimValues)
        {
            // Vérifier si l'utilisateur a au moins un claim parmi ceux attendus
            bool hasAccess = (await _claimRepository.GetClaimsValuesByUserIdAsync(userAuthId)).Any(c => claimValues.Contains(c));
            string? memberId = string.Empty;

            if (!hasAccess)
            {
                memberId = (await _accountRepository.FindMemberByUserIdAsync(userAuthId))?.Id.ToString();
            }
            var departs = await _departmentRepository.GetDepartAsync(memberId);
            departs.ShowInfo = hasAccess;
            return departs;
        }
    }
}