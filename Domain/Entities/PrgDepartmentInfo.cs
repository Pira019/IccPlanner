// Ignore Spelling: Prg


using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    ///     Programme de Département Info
    ///     Cette classe désigne les info d un département
    /// </summary>
    public class PrgDepartmentInfo : BaseEntity
    {
        public int Id { get; set; }
        public int DepartmentProgramId { get; set; }
        public DepartmentProgram DepartmentProgram { get; set; } = null!;
        public ICollection<PrgDate> PrgDate { get; set;  } = new List<PrgDate>(); 

        /// <summary>
        ///     Liste des jours associés au programme du département
        /// </summary>
        /// 
        [MaxLength(1)]
        public string? Day { get; set; }
    }
}
