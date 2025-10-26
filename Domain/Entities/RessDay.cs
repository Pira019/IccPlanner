using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    ///     Retourne les jours
    /// </summary>
    public class RessDay
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        ///     Libelle de traduction
        /// </summary>
        /// 
        [MaxLength(10)]
        public string Libelle { get; set; }
    }
}
