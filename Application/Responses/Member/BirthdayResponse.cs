namespace Application.Responses.Member
{
    /// <summary>
    ///     Anniversaire d'un membre du mois.
    /// </summary>
    public class BirthdayResponse
    {
        /// <summary>Prénom et initiale du nom.</summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>Jour et mois (format MM-dd).</summary>
        public string BirthDate { get; set; } = string.Empty;
        /// <summary>Jour du mois.</summary>
        public int Day { get; set; }
        /// <summary>Nom du département.</summary>
        public string DepartmentName { get; set; } = string.Empty;
    }
}
