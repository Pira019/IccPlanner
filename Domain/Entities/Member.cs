namespace Domain.Entities
{
    /// <summary>
    /// Cette classe définit un member, ex: un chantre etc ...
    /// </summary>
    public class Member : Person
    {
        public Guid Id { get; set; }
        public User? User { get; set; } 
        public DateOnly? EntryDate { get; set; }
        public Guid? AddedById { get; set; } // Id member
        public Member? AddedBy { get; set; } //  member
        public DateOnly? BirthDate { get; set; } 
        public List<Department> Departements { get; } = [];
    }
}
