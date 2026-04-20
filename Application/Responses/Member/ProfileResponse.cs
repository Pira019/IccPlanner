namespace Application.Responses.Member
{
    /// <summary>
    ///     Réponse du profil du membre connecté.
    /// </summary>
    public class ProfileResponse
    {
        /// <summary>Identifiant unique du membre.</summary>
        public Guid MemberId { get; set; }
        /// <summary>Prénom du membre.</summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>Nom de famille du membre.</summary>
        public string? LastName { get; set; }
        /// <summary>Sexe (M ou F).</summary>
        public string Sexe { get; set; } = string.Empty;
        /// <summary>Adresse e-mail du compte utilisateur.</summary>
        public string? Email { get; set; }
        /// <summary>Numéro de téléphone du compte utilisateur.</summary>
        public string? PhoneNumber { get; set; }
        /// <summary>Ville de résidence.</summary>
        public string? City { get; set; }
        /// <summary>Quartier de résidence.</summary>
        public string? Quarter { get; set; }
        /// <summary>Date de naissance (jour et mois, format MM-dd).</summary>
        public string? BirthDate { get; set; }
        /// <summary>Date d'entrée dans l'organisation.</summary>
        public DateOnly? EntryDate { get; set; }
        /// <summary>Liste des départements auxquels le membre appartient.</summary>
        public List<ProfileDepartmentResponse> Departments { get; set; } = [];
    }

    /// <summary>
    ///     Département du profil avec le ministère associé.
    /// </summary>
    public class ProfileDepartmentResponse
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string? MinistryName { get; set; }
    }
}
