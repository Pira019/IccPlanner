namespace Application.Requests.Member
{
    /// <summary>
    ///     Requête de modification du profil du membre connecté.
    /// </summary>
    public class UpdateProfileRequest
    {
        /// <summary>Prénom.</summary>
        public string? Name { get; set; }
        /// <summary>Nom de famille.</summary>
        public string? LastName { get; set; }
        /// <summary>Sexe (M ou F).</summary>
        public string? Sexe { get; set; }
        /// <summary>Ville.</summary>
        public string? City { get; set; }
        /// <summary>Quartier.</summary>
        public string? Quarter { get; set; }
        /// <summary>Date de naissance (jour et mois uniquement, format MM-dd).</summary>
        public string? BirthDate { get; set; }
        /// <summary>Numéro de téléphone.</summary>
        public string? PhoneNumber { get; set; }
    }
}
