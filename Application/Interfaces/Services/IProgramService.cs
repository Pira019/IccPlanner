using Application.Dtos;
using Application.Dtos.Program;
using Application.Requests.Program;
using Application.Responses.Program; 

namespace Application.Interfaces.Services
{
    public interface IProgramService
    {
        /// <summary>
        /// Permet d'ajouter un service
        /// </summary>
        /// <param name="request"> Model de donnée <see cref="AddProgramRequest"/> </param>
        /// <returns> <see cref="AddProgramResponse"/> </returns>
        public Task<AddProgramResponse> Add(AddProgramRequest request);

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


    }
}
