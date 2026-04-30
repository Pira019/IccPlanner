namespace Application.Responses.Department
{
    /// <summary>
    ///     Détails complets d'un département : infos, membres, postes, programmes.
    /// </summary>
    public class DepartmentDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ShortName { get; set; }
        public string? Description { get; set; }
        public DateOnly? StartDate { get; set; }
        public string? MinistryName { get; set; }
        public int MemberCount { get; set; }
        public int ProgramCount { get; set; }
        public List<DepartmentDetailMember> Members { get; set; } = [];
        public List<DepartmentDetailPoste> Postes { get; set; } = [];
        public List<DepartmentDetailProgram> Programs { get; set; } = [];
        public List<DepartmentDetailInvitation> Invitations { get; set; } = [];
    }

    public class DepartmentDetailMember
    {
        public int DepartmentMemberId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? Sexe { get; set; }
        public string? Status { get; set; }
        public List<string> Postes { get; set; } = [];
    }

    public class DepartmentDetailPoste
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ShortName { get; set; }
    }

    public class DepartmentDetailProgram
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public string? ShortName { get; set; }
        public bool IndRecurrent { get; set; }
    }

    /// <summary>
    ///     Invitation envoyée pour un département.
    /// </summary>
    public class DepartmentDetailInvitation
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateSend { get; set; }
        public DateTime DateExpiration { get; set; }
        public bool IndUsed { get; set; }
        public bool IndAct { get; set; }
        public bool IsExpired => !IndUsed && DateExpiration < DateTime.UtcNow;
    }
}
