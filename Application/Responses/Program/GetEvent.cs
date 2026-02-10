namespace Application.Responses.Program
{
    public class GetEvent
    {
        public int Id { get; set; }
        public int IdPrg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Date { get; set; }
        public bool IndRecurrent { get; set; }
    }
}
