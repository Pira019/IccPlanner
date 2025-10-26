// Ignore Spelling: Prg

using System.ComponentModel.DataAnnotations; 

namespace Domain.Entities
{
    /// <summary>
    /// Classe qui définit le programme
    /// </summary>
    public class DepartmentProgram : BaseEntity
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int ProgramId { get; set; }
        public PrgDepartmentInfo? PrgDepartmentInfo { get; set; }

        /// <summary>
        ///     Date de fin du programme. Peut être nulle si le programme est en cours ou indéfini.
        /// </summary>
        public DateOnly? DateEnd { get; set; }
        public Program Program { get; set; } = null!;
        public  Department Department { get; set; } = null!;

        /// <summary>
        ///  Indicateur qui indique si le programme est récurrent ou non.       
        ///  Quelque chose qui revient régulièrement, selon une fréquence fixe.
        /// </summary>      
        public required bool IndRecurent { get; set; }
        public bool IndActiv { get; set; } = true;   
        public string? Comment { get; set; } 
        public required Guid CreateById { get; set; }
        public Member? CreateBy { get; set; } = null!;
        public Member? UpdateBy { get; set; }
        public List<FeedBack>? FeedBacks{ get; set; }


    }
}
