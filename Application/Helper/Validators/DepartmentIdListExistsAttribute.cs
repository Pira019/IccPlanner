using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using Domain.Abstractions;

namespace Application.Helper.Validators
{
    public class DepartmentIdListExistsAttribute : ValidationAttribute
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentIdListExistsAttribute() { }

        public DepartmentIdListExistsAttribute(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var departementRepository = _repository ?? (IDepartmentRepository)validationContext.GetService(typeof(IDepartmentRepository))!;

            if (departementRepository == null && validationContext.Items.ContainsKey(typeof(IDepartmentRepository)))
            {
                departementRepository = validationContext.Items[typeof(IDepartmentRepository)] as IDepartmentRepository;
            }

            var departmentIds = value as string;

            // Convertir la chaîne en liste d'entiers
            var ids = Utiles.ConvertStringToArray(departmentIds!);

            var departmentExists = departementRepository!.GetValidDepartmentId(ids!);
            var invalidDepartments = ids.Select(id => (int?)id).Except(departmentExists);

            if (!ids.Any())
            { 
                return new ValidationResult(string.Format(DepartmentError.DEPARTMENT_INVALID.Message));
            }
            
            else if (invalidDepartments.Any())
            {
                var invalidDepartmentIds_ = string.Join(",", invalidDepartments);
                return new ValidationResult(string.Format(DepartmentError.DEPARTMENT_LIST_NOT_EXISTS.Message, invalidDepartmentIds_));
            }

            return ValidationResult.Success;
        }
    }
}
