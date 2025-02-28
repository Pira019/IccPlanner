using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using Domain.Abstractions;

namespace Application.Helper.Validators
{
    public class MemberExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var accountRepository = (IAccountRepository)validationContext.GetService(typeof(IAccountRepository))!;

            var memberId = value?.ToString();
            var memberExists = false;

            if (Guid.TryParse(memberId, out var guid))
            {
                memberExists = accountRepository.IsMemberExist(memberId!).Result;
            }

            if (!memberExists)
            {
                return new ValidationResult(AccountErrors.MEMBER_NOT_EXISTS.Message);
            }

            return ValidationResult.Success;
        }
    }
}
