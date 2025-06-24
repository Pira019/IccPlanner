

// Ignore Spelling: Prg

namespace Domain.Entities
{
    /// <summary>
    ///     Date de programme.
    /// </summary>
    public class PrgDate
    {
        public int Id { get; set; }
        public DateOnly? Date { get; set; }
        public string? Day { get; set; }
        public int PrgDepartmentInfoId { get; set; }
        public PrgDepartmentInfo PrgDepartmentInfo { get; set; } = null!;
        public List<TabServicePrg> TabServicePrgs { get; } = [];
    }
}
