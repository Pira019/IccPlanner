// Ignore Spelling: Prg
 
using Application.Requests.ServiceTab;
using Application.Responses.TabService;

namespace Application.Interfaces.Services
{
    public interface ITabServicePrgService
    {

        /// <summary>
        ///     Permet d'ajouter le service a un programme
        /// </summary>
        /// <param name="prgDepartmentRequest">
        ///     Corps de la requête <see cref="AddServicePrgDepartmentRequest"/>
        /// </param> 
        public Task AddServicePrg(AddServicePrgDepartmentRequest prgDepartmentRequest);

        /// <summary>
        ///     Permet de récupérer les dates d'un programme pour un utilisateur.
        /// </summary>
        /// <param name="userId">
        ///     User Id pour lequel on veut récupérer les dates.
        /// </param>
        /// <param name="month">
        ///  Mois pour lequel on veut récupérer les dates.
        /// </param>
        /// <param name="year">
        ///     Pour l'année pour laquelle on veut récupérer les dates.
        /// </param>
        /// <returns>
        ///     Retourne un objet <see cref="GetDatesResponse"/> contenant les dates du programme pour l'utilisateur spécifié.
        /// </returns>
        public Task<GetDatesResponse> GetDates( Guid? userId,int? month = null, int? year = null);  
    }
}
