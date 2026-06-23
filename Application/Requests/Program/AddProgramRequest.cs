namespace Application.Requests.Program
{
    /// <summary>
    /// Model de donnée pour ajouter un programme
    /// </summary>
    public class AddProgramRequest
    { 
        /// <summary>
        ///     Nom.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Abréviation ou nom court (ex: APP).
        /// </summary>
        public string? ShortName { get; set; }
        public required string Description { get; set; }

        /// <summary>
        ///     Si true, utilise le programme existant avec le même nom au lieu de bloquer.
        /// </summary>
        public bool ForceUseExisting { get; set; } = false;
    }
}
