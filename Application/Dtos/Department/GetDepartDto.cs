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
        public string? ShortName { get; set; }

        /// <summary>
        ///    Nombre de membres dans le département.
        /// </summary>
        public int NbrMember { get; set; }

        /// <summary>
        ///     Nombre de programmes associés au département.
        /// </summary>
        public int NbrProgram { get; set; } 
    }
}
