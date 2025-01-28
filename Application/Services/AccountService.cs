﻿using System.Text;
using Application.Dtos.Account;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests.Account;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.Services
{
    /// <summary>
    /// Ce service permet de gerer les actions d'un compte
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISendEmailService _sendEmailService;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, ISendEmailService sendEmailService)
        {
            _accountRepository = accountRepository;
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
            var result = await _accountRepository.CreateAsync(dto.User, dto.User.PasswordHash!);

            if (result.Succeeded)
            {
                var newUser = await _accountRepository.FindByEmailAsync(dto.User.Email!);
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

        public async Task<SignInResult> Login(LoginRequest loginRequest)
        {
            return await _accountRepository.SignIn(loginRequest.Email, loginRequest.Password, loginRequest.Remember);
        }
    }
}
