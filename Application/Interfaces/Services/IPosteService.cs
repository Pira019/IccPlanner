using Application.Interfaces.Repositories;
using Application.Requests.Poste;
using Application.Responses.Department;

namespace Application.Interfaces.Services
{
    public interface IPosteService
    {
        public Task<Result<PosteResponse>> AddAsync(AddPosteRequest request);
        public Task<Result<PosteResponse>> UpdateAsync(int id, AddPosteRequest request);
        public Task<List<PosteResponse>> GetAllAsync();
        public Task<Result<PosteResponse>> GetByIdAsync(int id);
        public Task<Result<bool>> DeleteAsync(int id);
    }
}
