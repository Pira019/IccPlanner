using System.Text;
using Application.Constants;
using Application.Dtos.Account;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Responses.Account;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Shared.Ressources;

namespace Application.Services
{
    /// <summary>
    /// Ce service permet de gérer les actions d'un compte
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISendEmailService _sendEmailService;
        private readonly IMapper _mapper; 
        private readonly IClaimRepository _claimRepository;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, ISendEmailService sendEmailService,
             IClaimRepository claimRepository)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _sendEmailService = sendEmailService; 
            _claimRepository = claimRepository;
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
    }
}
