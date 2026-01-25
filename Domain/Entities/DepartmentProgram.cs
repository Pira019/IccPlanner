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
        public List<PrgDepartmentInfo>? PrgDepartmentInfos { get; set; }
        public Program Program { get; set; } = null!;
        public  Department Department { get; set; } = null!;

        /// <summary>
        ///     Indique le type de programme Récurent ou Ponctuel.
        /// </summary>        
        public bool IndRecurent { get; set; }       
        public string? Comment { get; set; }

        /// <summary>
        ///     Date de début du programme (pour les programmes ponctuels).
        /// </summary>
        public DateOnly? DateS { get; set; }

        /// <summary>
        ///     Date de fin du programme (pour les programmes ponctuels).
        /// </summary>
        public DateOnly? DateF { get; set; }
        public required string CreateBy { get; set; } 
        public string? UpdateBy { get; set; }
        public List<FeedBack>? FeedBacks{ get; set; }


    }
}
