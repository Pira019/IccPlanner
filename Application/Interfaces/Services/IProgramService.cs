using Application.Dtos;
using Application.Dtos.Program;
using Application.Requests.Program;
using Application.Responses.Program; 

namespace Application.Interfaces.Services
{
    public interface IProgramService
    {
        /// <summary>
        ///     Ajouter un nouveau programme.
        /// </summary>
        /// <param name="request">
        ///     Model de requête <see cref="AddProgramRequest"/>
        /// </param>
        /// <param name="userId">
        ///     Indique l'identifiant de l'utilisateur qui effectue l'opération
        /// </param>
        /// <param name="permissionName">
        ///    Permet de spécifier le nom de la permission requise pour effectuer cette opération.
        /// </param>
        /// <returns>
        ///     Retourne un objet <see cref="Result{AddProgramResponse}"/>
        /// </returns>
        public Task<Result<AddProgramResponse>> Add(AddProgramRequest request, string userId, string permissionName);

        /// <summary>
        /// Permet de savoir si le nom du programme existe 
        /// </summary>
        /// <param name="programName">Le nom du programme</param>
        /// <returns>
        /// Retourne une valeur <see cref="bool"/>
        /// </returns>
        public Task<bool> IsNameExists(string programName);

        /// <summary>
        ///     Obtenir la liste de programme en pagination
        /// </summary>
        /// <param name="pageIndex">Index de la page</param>
        /// <param name="pageSize"></param>
        /// <returns>
        ///     Un objet DTO <see cref="PaginatedDto{ProgramDto}"/>
        /// </returns>
        public Task<PaginatedDto<ProgramDto>> GetPaginatedProgram(int pageIndex, int pageSize);

        /// <summary>
        ///     Modifier le programme
        /// </summary>
        /// <param name="idPrg"> 
        ///     Identifiant du programme à modifier.
        /// </param>
        /// <param name="request">
        ///     Model de requête <see cref="AddProgramRequest"/>
        /// </param>
        /// <param name="permissionName">
        ///     Specifie le nom de la permission requise pour effectuer cette opération.
        /// </param>
        /// <returns></returns>
        public Task<Result<bool>> Update(int idPrg, AddProgramRequest request, string userId, string permissionName);


    }
}
