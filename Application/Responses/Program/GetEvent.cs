namespace Application.Responses.Program
{
    public class GetEvent
    {
        public int Id { get; set; }
        public int IdPrg { get; set; }
        public int DepartmentId { get; set; }
        /// <summary>
        ///     Nom du département.
        /// </summary>
        public string? DepartmentName { get; set; }
        /// <summary>
        ///     Nom du programme.
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        ///     Date de l'événement.
        /// </summary>
        public String Date { get; set; }
        public bool IndRecurrent { get; set; }
    }
}
