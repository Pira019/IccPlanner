using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;

namespace Application.Services
{
    /// <summary>
    /// Permet de gérer les action CRUD
    /// </summary>
    public class BaseService<TEntity> : IBaseService where TEntity : class
    {
        private IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual async Task<Object> Add(Object requestBody)
        {
            //Map le request body en une classe définit 
            var newEntity = _mapper.Map<TEntity>(requestBody);
            return await _baseRepository.Insert(newEntity);
        }

        public virtual async Task<IEnumerable<Object>> GetAll()
        {
            return await _baseRepository.GetAllAsync();
        }
    }
}
