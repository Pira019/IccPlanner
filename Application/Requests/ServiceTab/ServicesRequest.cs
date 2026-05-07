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
        /// <summary>
        ///     Date exacte pour filtrer les services (format yyyy-MM-dd).
        /// </summary>
        public string? Date { get; set; }
        /// <summary>
        ///     Id du programme pour filtrer les services.
        /// </summary>
        public int? ProgramId { get; set; }
    }
}
