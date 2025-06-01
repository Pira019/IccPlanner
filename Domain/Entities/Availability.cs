namespace Domain.Entities
{
    /// <summary>
    ///     Cette classe permet de gérer la disponibilité des membres du département pour un service
    /// </summary>
    public class Availability : BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        ///     Id de service
        /// </summary>
        public int TabServicePrgId { get; set; }

        /// <summary>
        ///     Id de DepartmentMemberId
        /// </summary>
        public int DepartmentMemberId { get; set; }
        public TabServicePrg TabServicePrg { get; set; } = null!;
        public DepartmentMember DepartmentMember { get; set; } = null!;

        /// <summary>
        ///     Notes 
        /// </summary>
        public string? Notes { get; set; }
    }
}
