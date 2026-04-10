// Ignore Spelling: Prg

using System.ComponentModel.DataAnnotations;
using Shared.Utiles;

namespace Domain.Entities
{
    /// <summary>
    ///     Cette classe permet de créer de services des programmes pour des départements
    /// </summary>
    public class TabServicePrg : BaseEntity
    {
        public int Id { get; set; }
        public int TabServicesId { get; set; }
        public int PrgDateId { get; set; }
        public required string DisplayName { 
            get => _displayName;
            set => _displayName = SharedUtiles.CapitalizeFirstLetter(value);
        }
        private string _displayName = string.Empty;
        public TabServices TabServices { get; set; } = null!;
        public PrgDate PrgDate { get; set; } = null!;
        public TimeOnly? ArrivalTimeOfMember { get; set; } 

        [MaxLength(15)]
        public string? Days { get; set; }
        public string? Notes { get; set;}
        public List<DepartmentMember> DepartmentMembers { get; } = []; 
        public List<Availability> Availabilities { get; } = []; 
    }
}
