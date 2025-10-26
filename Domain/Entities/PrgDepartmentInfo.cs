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
        ///     Jours de programme régulier.
        /// </summary>
        public ICollection<PrgRecDay> PrgRecDays { get; set; } = new List<PrgRecDay>();

        /// <summary>
        ///     Indicateur si activé ou non.
        /// </summary>
        /// 
        public bool IndAct { get; set; } = true;
        /// <summary>
        ///     Jour de fin pour de programme régulier.
        /// </summary>
        public DateOnly? DateStart { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly? DateEnD { get; set; }
    }
}
