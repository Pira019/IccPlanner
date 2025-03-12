namespace Application.Dtos.Department
{
    /// <summary>
    /// Cette classe contient les informations 
    /// d'un programme  d'un département existants dans la base de données. 
    /// </summary>
    public class DepartmentProgramExistingDto
    {
        /// <summary>
        /// Identifiant unique du programme.
        /// </summary>
        public int ProgramId { get; set; }

        /// <summary>
        /// Identifiant unique du département associé à ce programme.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Date de début du programme dans le département.
        /// </summary>
        public DateOnly StartAt { get; set; }
    }
}
