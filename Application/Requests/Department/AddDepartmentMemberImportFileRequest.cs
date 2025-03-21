using System.ComponentModel.DataAnnotations;
using Application.Helper.Validators;
using Microsoft.AspNetCore.Http;

namespace Application.Requests.Department
{
    /// <summary>
    /// Cette classe est un model qui permet aux gestionnaires de up-loader un fichier pour ajouter des member
    /// </summary>
    public class AddDepartmentMemberImportFileRequest
    {
        [Display(Name = "File")]
        public required IFormFile formFile { get; set; }

        [DepartmentExists]
        public required int DepartmentId { get; set; }
    }
}
