// Ignore Spelling: Prg

using Application.Requests.ServiceTab;

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
    }
}
