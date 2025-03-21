// Ignore Spelling: Validators Validator 
using System.Text.RegularExpressions;
using Application.Requests.Department;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Shared.Abstraction;
using Shared.Interfaces;

namespace Application.Helper.Validators
{
    public class AddDepartmentMemberImportFileRequestValidator : AbstractValidator<AddDepartmentMemberImportFileRequest>
    {
        private readonly string[] _allowedExtensions = { ".xls", ".xlsx" };
        private const int MaxFileSize = 5 * 1024 * 1024; // 5 MB
        private readonly IRessourceLocalizer _localizer;
        public AddDepartmentMemberImportFileRequestValidator(IRessourceLocalizer ressourceLocalizer)
        {
            this._localizer = ressourceLocalizer;
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.formFile)
                .NotNull().WithMessage(_localizer.Localize(ValidationMessage.NOT_NULL.ToString()))
                .Must(file => file.Length > 0).WithMessage(_localizer.Localize(ValidationMessage.NOT_NULL.ToString()))
                .Must(file => _allowedExtensions.Contains(System.IO.Path.GetExtension(file.FileName))).WithMessage(_localizer.Localize(ValidationMessage.INVALID_FILE_EXTENSION.ToString()))
                .Must(ValidateExcelFile).WithMessage(_localizer.Localize(ValidationMessage.INVALID_FILE_EXTENSION.ToString()))
                .Must(file => file.Length < MaxFileSize).WithMessage(string.Format(_localizer.Localize(ValidationMessage.FILE_TOO_LARGE.ToString()), MaxFileSize))
                .Must(file =>
                    {
                        ErrorMessage = null;
                        return ValidateRequiredColumns(file);
                    }
                    ).WithMessage(file => ErrorMessage ?? _localizer.Localize(ValidationMessage.INVALID_FILE_DATA.ToString()));  // Validation des données dans le fichier
        }

        public string? ErrorMessage;

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

        private bool ValidateRequiredColumns(IFormFile file)
        {
            var requiredColumns = new Dictionary<string, string>
            {
                { "prenom", "PRENOM" },
                { "sexe", "SEXE" },
                { "contact", "CONTACT" }
            };

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    foreach (var worksheet in package.Workbook.Worksheets)
                    {

                        // Lire les en-têtes de colonne
                        var columns = worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column]
                                        .Select(c => c.Text.Trim().ToLower()).ToList();

                        // Vérifier que toutes les colonnes requises sont présentes
                        var columnIndexes = new Dictionary<string, int>();
                        foreach (var key in requiredColumns.Keys)
                        {
                            int index = columns.IndexOf(requiredColumns[key].ToLower());
                            if (index == -1)
                            {
                                ErrorMessage = _localizer.Localize(ValidationMessage.MISSING_REQUIRED_COLUMN.ToString());
                                return false;
                            }
                            columnIndexes[key] = index + 1; // Excel est indexé à 1
                        }

                        // Vérifier chaque ligne de données
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var nom = worksheet.Cells[row, columnIndexes["prenom"]].Text;
                            var sexe = worksheet.Cells[row, columnIndexes["sexe"]].Text.Trim();
                            var numero = worksheet.Cells[row, columnIndexes["contact"]].Text.Trim();

                            var errorOn = string.Format(_localizer.Localize(ValidationMessage.INVALID_ON.ToString()), row);

                            // Validation du prénom (autorise les espaces et apostrophes)
                            if (string.IsNullOrEmpty(nom) || !Regex.IsMatch(nom, @"^[A-Za-zÀ-ÖØ-öø-ÿ' -]+$"))
                            {
                                ErrorMessage = _localizer.Localize(ValidationMessage.INVALID_NAME.ToString()) + errorOn;
                                return false;
                            }

                            // Validation du sexe (accepte plusieurs variations)
                            var validGenders = new HashSet<string> { "h", "homme", "m", "masculin", "f", "femme", "féminin" };
                            if (string.IsNullOrEmpty(sexe) || !validGenders.Contains(sexe.ToLower()))
                            {
                                ErrorMessage = _localizer.Localize(ValidationMessage.INVALID_GENDER.ToString()) + errorOn;
                                return false;
                            }

                            // Validation du numéro de téléphone (uniquement chiffres)
                            if (string.IsNullOrEmpty(numero) || !numero.All(char.IsDigit))
                            {
                                ErrorMessage = _localizer.Localize(ValidationMessage.INVALID_PHONE_NUMBER.ToString()) + errorOn;
                                return false;
                            }
                        }
                    }
                }
            }

            return true; // Toutes les validations passent
        }


    }
}
