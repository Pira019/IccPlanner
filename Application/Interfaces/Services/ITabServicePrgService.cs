// Ignore Spelling: Prg
 
using Application.Requests.ServiceTab;
using Application.Responses.ServicePrg;
using Application.Responses.TabService;
using Microsoft.AspNetCore.Http.HttpResults;

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
        public Task<Result<bool>> AddServicePrg(AddServicePrgDepartmentRequest prgDepartmentRequest);

        /// <summary>
        ///     Obtient les dates des programme pour un département donné, un mois et une année donnés
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="IdDepart"></param>
        /// <returns>
        ///     Retourne une liste de dates correspondant aux programmes du département pour le mois et l'année spécifiés, ou une erreur en cas d'échec de l'opération.
        /// </returns>
        public Task<Result<List<GetDatesResponse>>> GetDatesByDepartAsync( int month, int year, int IdDepart);  
        public Task<Result<List<GetServicesListResponse>>> GetPrgServices( ServicesRequest request);
        public Task<Result<GetServiceByDepart?>> GetServicePrgByDepartAsync(int idDepart, DateOnly dateOnly, Guid? memberId);
    }
}
