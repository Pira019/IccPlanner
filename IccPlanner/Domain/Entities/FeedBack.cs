namespace IccPlanner.Domain.Entities
{
    public class FeedBack : BaseEntity
    {
        public int Id {  get; set; }
        public int DepartementId { get; set; }
        public int ProgramDepartmentId { get; set; }
        public string Comment { get; set; }
        public int ? rating { get; set; }
        public DateTime SubmitAt { get; set; }
    }
}
