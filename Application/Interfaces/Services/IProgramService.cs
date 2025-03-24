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
    }
}
