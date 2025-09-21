namespace Application.Dtos.Department
{
    /// <summary>
    ///     Dto pour obtenir les informations d'un département
    /// </summary>
    public class GetDepartDto
    {
        /// <summary>
        ///     Identifiant unique du département.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Nom du département.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        ///     Responsable du département.
        /// </summary>
        public string? Manager { get; set; }

        /// <summary>
        ///    Nombre de membres dans le département.
        /// </summary>
        public int NbrMember { get; set; }

        /// <summary>
        ///     Indique si le département est actif.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
