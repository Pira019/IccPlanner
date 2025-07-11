using Application.Dtos.Department;
using Application.Dtos.TabServicePrgDto;

namespace Application.Dtos.PrgDate
{
    public class PrgDateDto
    {
        public DateOnly? Date { get; set; }
        public DepartmentDto? Department { get; set; }
        public int NbrService { get; set; }

        /// <summary>
        ///     Représente la liste des services associés à cette date de programme.
        /// </summary>
        public IEnumerable<ServicePrgDateDto>? Services { get; set; }
    }
}
