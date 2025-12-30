using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;

namespace Application.Services
{
    /// <summary>
    /// Permet de gérer les action CRUD
    /// </summary>
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual async Task<TEntity> Add(TEntity requestBody)
        {
            //Map le request body en une classe définit 
            var newEntity = _mapper.Map<TEntity>(requestBody);
            return await _baseRepository.Insert(newEntity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task DeleteSoftByIdAsync(int id)
        {
            await _baseRepository.DeleteSoftAsync(id);
        }
    }
}
