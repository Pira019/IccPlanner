// Ignore Spelling: Prg

using System.ComponentModel.DataAnnotations;
using Shared.Utiles;

namespace Domain.Entities
{
    public class TabServices
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        /// <summary>
        /// Heure d'arriver des membres
        /// </summary>
        public TimeOnly? ArrivalTimeOfMember { get; set; }

        /// <summary>
        /// Text a afficher
        /// </summary>
        /// 
        [MaxLength(55)]
        public required string DisplayName { 
            get => _displayName;
            set => _displayName = Utiles.CapitalizeFirstLetter(value);
        }

        public string? Notes { get; set; }

        private string _displayName = string.Empty;

    }
} 