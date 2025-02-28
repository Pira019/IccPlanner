using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using Domain.Abstractions;

namespace Application.Helper.Validators
{
    public class DepartmentExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var departementRepository = (IDepartmentRepository)validationContext.GetService(typeof(IDepartmentRepository))!;

            if (departementRepository == null && validationContext.Items.ContainsKey(typeof(IDepartmentRepository)))
            {
                departementRepository = validationContext.Items[typeof(IDepartmentRepository)] as IDepartmentRepository;
            }

            var departmentId = (int)value!;
            var depatmentExists =  departmentId != 0 ?  departementRepository!.IsDepartmentIdExists(departmentId).Result : false;

            if (!depatmentExists) 
            {
                return new ValidationResult(DepartmentError.DEPARTMENT_NOT_EXISTS.Message);
            }

            return ValidationResult.Success;
        }
    }
}
