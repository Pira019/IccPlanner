
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Abstractions
{
    [NotMapped]
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
        public string Code {  get; set; }

        /// <summary>
        /// Message de l'erreur
        /// </summary>
        public string Message {  get; set; }
    }
}
