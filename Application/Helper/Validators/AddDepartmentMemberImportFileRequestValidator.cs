// Ignore Spelling: Validators Validator 
using Application.Requests.Department;
using FluentValidation;
using Microsoft.AspNetCore.Http; 
using OfficeOpenXml;
using Shared.Abstraction;
using Shared.Interfaces; 

namespace Application.Helper.Validators
{
    public class AddDepartmentMemberImportFileRequestValidator :  AbstractValidator<AddDepartmentMemberImportFileRequest>
    { 
        private readonly string[] _allowedExtensions = { ".xls", ".xlsx" };
        private const int MaxFileSize = 5 * 1024 * 1024; // 5 MB
        public AddDepartmentMemberImportFileRequestValidator(IRessourceLocalizer ressourceLocalizer)
        {  
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.formFile)
                .NotNull().WithMessage(ressourceLocalizer.Localize(ValidationMessage.NOT_NULL.ToString()))
                .Must(file => file.Length > 0).WithMessage(ressourceLocalizer.Localize(ValidationMessage.NOT_NULL.ToString()))
                .Must(file => _allowedExtensions.Contains(System.IO.Path.GetExtension(file.FileName))).WithMessage(ressourceLocalizer.Localize(ValidationMessage.INVALID_FILE_EXTENSION.ToString()))
                .Must(ValidateExcelFile).WithMessage(ressourceLocalizer.Localize(ValidationMessage.INVALID_FILE_EXTENSION.ToString()))
                .Must(file => file.Length < MaxFileSize).WithMessage(string.Format(ressourceLocalizer.Localize(ValidationMessage.FILE_TOO_LARGE.ToString()),MaxFileSize));
        }

        // Fonction pour valider que le fichier est bien un fichier Excel
        private bool ValidateExcelFile(IFormFile file)
        {
            try
            {
                if (file == null) return false;

                using (var package = new ExcelPackage(file.OpenReadStream())) // Lit le fichier Excel
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    return worksheet != null; // Vérifie qu'il y a au moins une feuille dans le fichier Excel
                }
            }
            catch
            {
                return false; // Si une exception est levée, le fichier n'est pas valide
            }
        }
    }
}
