﻿using IccPlanner.Domain.Enums;

namespace IccPlanner.Domain.Entities
{
    public class DepartmentMember : BaseEntity
    {
        private Guid MemberId { get; set; }
        private int DepartementId { get; set; }
        public Member Member { get; set; } = null!;
        public Departement Departement { get; set; } = null!;
        private string NickName {  get; set; }
        private DateOnly DateEnty { get; set; }
        private MemberStatusEnum Staus { get; set; }       
        private List<ProgramDepartment> ProgramDepartments { get; } = [];
        private List<FeedBack> FeedBacks { get; } = [];
        private List<Availability> Availabilities { get; } = [];


    }
}
