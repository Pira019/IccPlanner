namespace Application.Dtos.AvailabilityDto
{
    public class GetAvailabityDto
    {
        public int? Id { get; set; }
        public DateOnly? DatePrg { get; set; }
        public bool IsPlanned { get; set; }
    }
}
