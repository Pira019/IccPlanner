namespace Domain.Entities
{
    /// <summary>
    /// Classe associative pour connaître le poste d'un membre dans un département
    /// </summary>
    public class DepartmentMemberPost
    {
        public int Id { get; set; }
        public int DepartmentMemberId { get; set; }
        public int PosteId { get; set; }
        public DateOnly? StartAt { get; set; }
        public DateOnly? EndAt { get; set; }

        /// <summary>
        ///     Indique si le membre souhaite recevoir le planning par email lors de la publication.
        /// </summary>
        public bool IndAutoPlanning { get; set; }

        public Poste Poste { get; set; } = null!; 
        public DepartmentMember DepartmentMember { get; set; } = null!;
    }
}
