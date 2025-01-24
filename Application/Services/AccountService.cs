using Application.Dtos.Account;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests.Account;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    /// <summary>
    /// Ce service permet de gerer les actions d'un compte
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository AccountRepository;
        private readonly ISendEmailService _sendEmailService;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, ISendEmailService sendEmailService)
        {
            AccountRepository = accountRepository;
            _mapper = mapper;
            _sendEmailService = sendEmailService;
        }

        /// <summary>
        /// Créer un compte 
        /// </summary>
        /// <param name="request">
        ///  Body de la requette 
        /// </param>
        /// <returns>Returne objet IdentityResult</returns>
        public async Task<IdentityResult> CreateAccount(CreateAccountRequest request)
        {
            var dto = _mapper.Map<CreateAccountDto>(request);
            var result = await AccountRepository.CreateAsync(dto.User, dto.User.PasswordHash!);

            if (result.Succeeded)
            {
                var newUser = await AccountRepository.FindByEmailAsync(dto.User.Email!);
                // Envoie Email
                _sendEmailService.SendEmailConfirmation(newUser!);
            }
            return result;
        }
    }
}
