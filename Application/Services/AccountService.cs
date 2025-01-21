using Application.Dtos.Account;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests.Account; 
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository AccountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            AccountRepository = accountRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Créer un compte 
        /// </summary>
        /// <param name="request">
        ///  Body de la requette 
        /// </param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAccount(CreateAccountRequest request)
        {
            var dto = _mapper.Map<CreateAccountDTO>(request); 
            return await AccountRepository.CreateAsync(dto.User,dto.User.PasswordHash!); 
        }
    }
}
