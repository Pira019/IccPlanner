using System.Text;
using Application.Constants;
using Application.Dtos.Account;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Responses.Account;
using AutoMapper;
using AutoMapper.Execution;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Shared.Ressources;

namespace Application.Services
{
    /// <summary>
    /// Ce service permet de gérer les actions d'un compte
    /// </summary>
    public class AccountService : BaseService<User> , IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISendEmailService _sendEmailService; 
        private readonly IClaimRepository _claimRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentMemberRepository _departmentMemberRepository;

        public AccountService(
            IAccountRepository accountRepository,
            ISendEmailService sendEmailService,
            IClaimRepository claimRepository,
            IInvitationRepository invitationRepository,
            IDepartmentRepository departmentRepository,
            IDepartmentMemberRepository departmentMemberRepository,
            IBaseRepository<User> baseRepository, IMapper mapper, IHttpContextAccessor? httpContextAccessor = null) : base(baseRepository, mapper, httpContextAccessor)
        {
            _accountRepository = accountRepository; 
            _sendEmailService = sendEmailService;
            _claimRepository = claimRepository;
            _departmentRepository = departmentRepository;
            _departmentMemberRepository = departmentMemberRepository;
            _invitationRepository = invitationRepository;
        }



        /// <summary>
        /// Créer un compte 
        /// </summary>
        /// <param name="request">
        ///  Body de la requête 
        /// </param>
        /// <returns>Retourne objet IdentityResult</returns>
        public async Task<IdentityResult> CreateAccount(CreateAccountRequest request, bool isAdmin = false)
        {
            var dto = _mapper.Map<CreateAccountDto>(request);
            var result = await _accountRepository.CreateAsync(dto.User!, request.Password);

            if (result.Succeeded)
            {
                var newUser = await FindUserAccountByEmail(request.Email);

                if (isAdmin)
                {
                    await _accountRepository.AddUserRole(newUser!, RolesConstants.ADMIN);
                }
                // Envoie Email
                await _sendEmailService.SendEmailConfirmation(newUser!);
            }
            return result;
        }

        public async Task<IdentityResult> ConfirmEmailAccount(User user, string code)
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            return await _accountRepository.ConfirmAccountEmailAsync(user!, code);
        }

        public async Task<User?> FindUserAccountById(string userId)
        {
            return await _accountRepository.FindByIdAsync(userId);
        }

        public async Task<Result<LoginAccountResponse>> Login(LoginRequest loginRequest)
        {
            var result = await _accountRepository.SignIn(loginRequest.Email, loginRequest.Password);


            if (!result.Succeeded)
            {
                return Result<LoginAccountResponse>.Fail(ValidationMessages.INVALID_LOGIN_ATTEMPT);
            }

            if (result.IsLockedOut)
            {
                return Result<LoginAccountResponse>.Fail(ValidationMessages.USER_IS_LOCKED_OUT);
            }
            
            var user = await _accountRepository.FindByEmailAsync(loginRequest.Email);
            var roles = await _accountRepository.GetUserRoles(user!); // Récupérer les rôles de l'utilisateur
            var userClaims = await _claimRepository.GetClaimsValuesByUserIdAsync(user!.Id); // Récupérer les claims(permissions) de l'utilisateur

            var response = new LoginAccountResponse
            {
                userId = user!.Id,
                roles = roles.ToList(),
                claims = userClaims.ToList()
            };

            return Result<LoginAccountResponse>.Success(response); 
        }

        public async Task<User?> FindUserAccountByEmail(string email)
        {
            return await _accountRepository.FindByEmailAsync(email);
        }

        public async Task<bool> IsAdminExistsAsync()
        {
            return await _accountRepository.IsAdminExistsAsync();
        }

        public async Task<ICollection<string>> GetUserRoles(User user)
        {
            return await _accountRepository.GetUserRoles(user);
        }

        public ClaimsResponse GetUserClaims()
        {
            return _claimRepository.GetUserClaims();
        }

        /// <summary>
        ///     Permet de créer un compte.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<Result<IdentityResult>> CreateAccountAsync(User user, string password)
        {
            //1a Verifier si l`email est déjà utilisé
            var existingUser = await _accountRepository.FindByEmailAsync(user.Email!);

            if (existingUser != null)
            {
                return Result<IdentityResult>.Fail(ValidationMessages.AJ_EmailExiste);
            }

            //1b Verifier si le numéro de téléphone est déjà utilisé
            var existingPhoneUser = user.PhoneNumber != null ?  await _accountRepository.IsTelExistAsync(user.PhoneNumber!) : false;

            if (existingPhoneUser)
            {
                return Result<IdentityResult>.Fail(ValidationMessages.AJ_TelExist);
            }
             
            var result = await _accountRepository.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return Result<IdentityResult>.Fail(result.Errors.Select(e => e.Description).First());
            }

            return Result<IdentityResult>.Success(result);
        }

        public async Task<Result<bool>> CreateIntvAccount(CreateInvAccountRequest request)
        {
            //1a : Vérifier si l'invitation existe et est valide

            var invitation = await _invitationRepository.GetByIdAsync(request.InvitationId);

            if(invitation == null || !invitation.IndAct || invitation.DateExpiration < DateTime.UtcNow)
            {
                return Result<bool>.Fail(ValidationMessages.AJ_IdInvNonExist);
            }

            //1b Vérifier si l'invitation est déjà utilisée

            if(invitation.IndUsed) {

                return Result<bool>.Fail(ValidationMessages.AJ_IdInvUsed);
            }

            //1b : Vérifier si le département existe
            bool isDepartmentExist = await _departmentRepository.IsExistAsync(invitation.DepartmentId);

            if (!isDepartmentExist) { 

                return Result<bool>.Fail(ValidationMessages.AJ_IdDepartNotFound);
            }

            //1d le code est invalide 
            if (invitation.Code != request.Code)
            {                 
                return Result<bool>.Fail(ValidationMessages.AJ_CodeIncorrect);
            } 

            request.Email = invitation.Email;

            var dto = _mapper.Map<CreateAccountDto>(request);

            var result = await CreateAccountAsync(dto.User!, request.Password);

            if (!result.IsSuccess) {                 
                return Result<bool>.Fail( result.Error);
            }

            var departmentMember = _mapper.Map<DepartmentMember>(
            dto,
            opts => opts.Items["DepartmentId"] = invitation.DepartmentId
            );

            await _departmentMemberRepository.Insert(departmentMember);
            // Ajouter le membre au département
           await  _invitationRepository.MarkAsUsedAsync(invitation.Id); // Marquer l'invitation comme utilisée
             
           return Result<bool>.Success(true);
        }
    }
}
