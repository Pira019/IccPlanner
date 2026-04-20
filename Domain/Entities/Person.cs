using System.ComponentModel.DataAnnotations;
using Shared.Utiles;

namespace Domain.Entities
{
    /// <summary>
    /// Cette classe definit une personne
    /// </summary>
    public abstract class Person : BaseEntity
    {
        private string _name = string.Empty;
        private string? _lastName;
        private string? _city;
        private string? _quarter;

        [MaxLength(55)]
        public required string Name
        {
            get => _name;
            set => _name = SharedUtiles.CapitalizeFirstLetter(value);
        }

        [MaxLength(55)]
        public string? LastName
        {
            get => _lastName;
            set => _lastName = value != null ? SharedUtiles.CapitalizeFirstLetter(value) : null;
        }

        [MaxLength(1)]
        public required string Sexe { get; set; }

        [MaxLength(55)]
        public string? City
        {
            get => _city;
            set => _city = value != null ? SharedUtiles.CapitalizeFirstLetter(value) : null;
        }

        [MaxLength(55)]
        public string? Quarter
        {
            get => _quarter;
            set => _quarter = value != null ? SharedUtiles.CapitalizeFirstLetter(value) : null;
        }
    }
}
