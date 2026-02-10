// Ignore Spelling: Auth 
using System.Globalization;
using System.Text.Json;
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
using Shared.Ressources;

namespace Application.Services
{
    /// <summary>
    /// Ce service permet de gérer les action d'un Département 
    /// </summary>
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IDepartmentProgramRepository _departmentProgramRepository;
        private readonly IClaimRepository _claimRepository;
        private readonly IMinistryRepository _ministryRepository;
        private readonly IProgramRepository _programRepository;

        public DepartmentService(IBaseRepository<Department> baseRepository, IMapper mapper, IDepartmentRepository departmentRepository, IPostRepository postRepository,
            IAccountRepository accountRepository, IDepartmentProgramRepository departmentProgramRepository,
            IClaimRepository claimRepository, IMinistryRepository ministryRepository,
            IProgramRepository programRepository )
            : base(baseRepository, mapper)
        {
            _departmentRepository = departmentRepository;
            _postRepository = postRepository;
            _accountRepository = accountRepository;
            _departmentProgramRepository = departmentProgramRepository;
            _claimRepository = claimRepository;
            _ministryRepository = ministryRepository;
            _programRepository = programRepository;
        }

        public async Task<Result<AddDepartmentResponse>> AddDepartment(AddDepartmentRequest addDepartmentRequest)
        {
            // Vérifie si le ministère existe
            var ministry = await _ministryRepository.GetByIdAsync(addDepartmentRequest.MinistryId);
            if (addDepartmentRequest.MinistryId != default && ministry == null)
            {
                return Result<AddDepartmentResponse>.Fail(string.Format(ValidationMessages.INVALID_VALUE, ValidationMessages.MINISTRY));
            }

            var existingNameDepartment = await _departmentRepository.IsNameExistsAsync(addDepartmentRequest.Name);

            if (existingNameDepartment)
            {
                return Result<AddDepartmentResponse>.Fail(string.Format(ValidationMessages.DEPARTMENT_EXIST, addDepartmentRequest.Name));
            }

            var departemtDto = _mapper.Map<Department>(addDepartmentRequest);
            departemtDto.MinistryId = addDepartmentRequest.MinistryId == 0 ? null : addDepartmentRequest.MinistryId;
            var newDepartment = await _departmentRepository.Insert(departemtDto);

            return Result<AddDepartmentResponse>.Success(_mapper.Map<AddDepartmentResponse>(newDepartment));
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

        public async Task<Result<bool>> AddDepartmentProgram(AddDepartmentProgramRequest departmentProgramRequest, string userId)
        {
            var valDept = await IsValidDepartmentIds(departmentProgramRequest.DepartmentIds);
            //Check si les départements sont vides
            if (!valDept)
            {
                return Result<bool>.Fail(ValidationMessages.DEPARTMENT_INVALID_IDS); 
            }

            var prgExis = await _programRepository.IsExistAsync(departmentProgramRequest.ProgramId);

            //Check si le programme existe
            if (!prgExis) {
                return Result<bool>.Fail(ValidationMessages.PRG_NOT_EXIST); 
            }

            //Check si le programme existe
            var departmentProgram = await _departmentProgramRepository.FindDepartmentProgramAsync(departmentProgramRequest.DepartmentIds, departmentProgramRequest.ProgramId, departmentProgramRequest.IndRecurrent);

            if (departmentProgram != null) {
                var deptName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(departmentProgram.Department.Name.ToLower());

                return Result<bool>.Fail(String.Format(ValidationMessages.DEPARTMENT_PROGRAM_EXIST, deptName , 
                      CultureInfo.CurrentCulture.TextInfo.ToTitleCase(departmentProgram.Program.Name.ToLower())));
            }


            var newDepartmentPrograms = await InitializeDepartmentProgramModel(departmentProgramRequest, userId);
            await _departmentProgramRepository.InsertAllAsync(newDepartmentPrograms);

            return Result<bool>.Success(true);
        }

        public async Task<bool> IsNameExists(string name)
        {
            return await _departmentRepository.IsNameExistsAsync(name);
        }

        /// <summary>
        ///     Initialise le modèle DepartmentProgram en fonction de la requête.
        /// </summary>
        /// <param name="request">
        ///     Modèle de requête pour ajouter un DepartmentProgram.
        /// </param>
        /// <param name="userId">
        ///     Indique l'ID de l'utilisateur qui crée le DepartmentProgram.
        /// </param>
        /// <returns>
        ///     Retourne une liste de DepartmentProgram initialisés.
        /// </returns>
        private async Task<IEnumerable<DepartmentProgram>> InitializeDepartmentProgramModel(AddDepartmentProgramRequest request, string userId)
        { 
            var result = new List<DepartmentProgram>();
            // Parse start date
            var startDate = string.IsNullOrWhiteSpace(request.DateStart)
                            ? DateOnly.FromDateTime(DateTime.Today)
                            : DateOnly.Parse(request.DateStart);

            // Parse end date (nullable)
            var endDate = DateOnly.TryParse(request.DateEnd, out var parsedEnd) ? parsedEnd : (DateOnly?)null;

            foreach (var departmentId in request.DepartmentIds.Distinct())
            {
                var departmentProgram = new DepartmentProgram
                {
                    DepartmentId = departmentId,
                    ProgramId = request.ProgramId,
                    CreateBy =userId,
                    Comment = request.Comment,
                    IndRecurent = request.IndRecurrent,
                    DateS = startDate,
                    DateF = endDate,
                    PrgDepartmentInfos = new List<PrgDepartmentInfo>()
                };

                if (request.IndRecurrent && request.Days != null && request.Days.Any())
                {
                    // Un PrgDepartmentInfo par jour récurrent
                    foreach (var day in request.Days.Distinct())
                    {
                        departmentProgram.PrgDepartmentInfos.Add(new PrgDepartmentInfo
                        {
                            Day = day.ToString(), // ou utiliser byte si possible
                            PrgDate = GenerateRecurrentDates(new List<int> { day }, request.DateEnd, startDate)
                        });
                    }
                }
                else if (!request.IndRecurrent && request.Dates != null && request.Dates.Any())
                {
                    // Programme ponctuel
                    departmentProgram.PrgDepartmentInfos.Add(new PrgDepartmentInfo
                    {
                        PrgDate = request.Dates
                            .Select(d => new PrgDate
                            {
                                Date = DateOnly.ParseExact(d, "yyyy-MM-dd")
                            })
                            .ToList()
                    });
                }

                result.Add(departmentProgram);
            }

            return result;

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
            }
            ;
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

        public async Task<GetDepartResponse> GetAsync(string userAuthId, string claimValue, int? pageNumber = null, int? pageSize = null)
        {
            var userClaims = Utiles.DeserializePermissions(_claimRepository.GetUserClaims()?.Permissions);   

            if (userClaims != null && userClaims.Contains(claimValue))
            {
                return await _departmentRepository.GetDepartAsync(null, pageNumber, pageSize);
            }

            var memberId = await _accountRepository.FindMemberByUserIdAsync(userAuthId);

            return await _departmentRepository.GetDepartAsync(memberId?.Id.ToString(), pageNumber, pageSize);
        }

        public async Task<Result<bool>> UpdateDept(int id, AddDepartmentRequest addDepartmentRequest)
        {
            // Vérifie si le ministère existe
            var ministry = await _ministryRepository.GetByIdAsync(addDepartmentRequest.MinistryId);
            if (addDepartmentRequest.MinistryId != default && ministry == null)
            {
                return Result<bool>.Fail(string.Format(ValidationMessages.INVALID_VALUE, ValidationMessages.MINISTRY));
            }

            var existingDepartment = await _departmentRepository.GetByIdAsync(id);
            if (existingDepartment == null)
            {
                return Result<bool>.Fail(ValidationMessages.DEPARTMENT_NOT_EXIST);
            }
            var namedExists = await _departmentRepository.IsNameExistsAsync(addDepartmentRequest.Name);
            // vérifie si le nom existe deja
            if (existingDepartment.Name.ToLower() != addDepartmentRequest.Name.ToLower() && namedExists)
            {
                return Result<bool>.Fail(string.Format(ValidationMessages.DEPARTMENT_EXIST, addDepartmentRequest.Name));
            }

            _mapper.Map(addDepartmentRequest,existingDepartment);
            existingDepartment.MinistryId = addDepartmentRequest.MinistryId == 0 ? null : addDepartmentRequest.MinistryId;
            existingDepartment.UpdatedAt = DateTime.UtcNow;
            await _departmentRepository.UpdateAsync(existingDepartment);
            return Result<bool>.Success(default);
        }

        /// <summary>
        ///   Genère une liste de dates récurrentes basées sur les jours spécifiés jusqu'à une date de fin donnée.
        /// </summary>
        /// <param name="days"> 
        /// Liste des jours de la semaine (0 = Dimanche, 1 = Lundi, ..., 6 = Samedi)
        /// </param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<PrgDate> GenerateRecurrentDates(
                                    List<int> days,          // jours récurrents : 0 = dimanche ... 6 = samedi
                                    string? endDate,          // date de fin en string (yyyy-MM-dd)
                                    DateOnly? startDate = null) // date de début optionnelle, défaut = aujourd'hui
        { 
            if (!DateOnly.TryParse(endDate, out var end))
                return new List<PrgDate>();

            if (days == null || days.Count == 0)    
                return new List<PrgDate>();

            // Date de début = startDate si fourni, sinon aujourd'hui
            var current = startDate ?? DateOnly.FromDateTime(DateTime.Today);

            var dates = new List<PrgDate>();

            while (current <= end)
            {
                if (days.Contains((int)current.DayOfWeek))
                {
                    dates.Add(new PrgDate { Date = current });
                }

                current = current.AddDays(1);
            }

            return dates;
        }

        public Task<DeptResponse> GetByIdAsync(int idDept)
        {
            var dept =  _departmentRepository.GetByIdAsync(idDept);
            return dept.ContinueWith(t => _mapper.Map<DeptResponse>(t.Result) );
        }
    }
}