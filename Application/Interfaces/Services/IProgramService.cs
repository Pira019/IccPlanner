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
    }
}
