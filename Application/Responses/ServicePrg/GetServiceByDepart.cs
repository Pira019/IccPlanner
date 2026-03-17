namespace Application.Responses.ServicePrg
{
    public class GetServiceByDepart
    {
        public DateOnly? Date { get; set; }
        public List<ServicePrgLst> ServicePrgDates { get; set; }
    }
}
