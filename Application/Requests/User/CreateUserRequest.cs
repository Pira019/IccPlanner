

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.User
{
    public class CreateUserRequest
    {
        [MaxLength(55)]
        public string Name { get; set; }
        [MaxLength(55)]
        public string? LastName { get; set; }
        [MaxLength(10)]
        public string Sexe { get; set; }
        [MaxLength(15)]
        [DisplayName(("Téléphone"))]
        public string Tel { get; set; } // numéro de téléphone
        [MaxLength(55)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(55)]
        public string? City { get; set; }
        [MaxLength(55)]
        public string? Quarter { get; set; } // Quartier 
        public DateOnly? EntryDate { get; set; } 
        public DateOnly? BirthDate { get; set; }  
    }
}
