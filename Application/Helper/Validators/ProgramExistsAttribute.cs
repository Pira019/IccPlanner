using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using Domain.Abstractions; 

namespace Application.Helper.Validators
{
    public class ProgramExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var programtRepository = (IProgramRepository)validationContext.GetService(typeof(IProgramRepository))!;

            if (programtRepository == null && validationContext.Items.ContainsKey(typeof(IDepartmentRepository)))
            {
                programtRepository = validationContext.Items[typeof(IProgramRepository)] as IProgramRepository;
            }

            var programId = (int)value!;
            var programExists = programtRepository!.IsExist(programId).Result;

            return programExists ? ValidationResult.Success
                : new ValidationResult(ProgramError.PROGRAM_NOT_EXISTS.Message);
        }
    }
}
