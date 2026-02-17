namespace Application.Requests.ServiceTab
{
    /// <summary>
    ///     Modele 
    /// </summary>
    public class ServicesRequest
    {
        public string? Title { get; set; }
        public bool? IndRecureent { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
    }
}
