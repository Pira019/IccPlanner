using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    ///     Classe représentant les jours de la semaine.
    /// </summary>
    public class PrgRecDay
    {
        /// <summary>
        ///    Identifiant .
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Nom du jour (ex: 1-7).
        /// </summary>
        /// 
        [MaxLength(1)]
        public string Day { get; set; }
        public int PrgDepartmentInfoId { get; set; }
        public PrgDepartmentInfo PrgDepartmentInfo { get; set; } = null!;
    }
}
