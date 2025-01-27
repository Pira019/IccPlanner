
namespace Domain.Abstractions
{
    /// <summary>
    /// Cette classe permet de gerer les erreur
    /// </summary>
    public class Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
        /// <summary>
        /// Le code de l'erreur
        /// </summary>
        protected string Code {  get; set; }

        /// <summary>
        /// Message de l'erreur
        /// </summary>
        protected string Message {  get; set; }
    }
}
