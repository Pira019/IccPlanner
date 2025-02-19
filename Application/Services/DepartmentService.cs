using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Department;
using Application.Responses.Department;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    /// <summary>
    /// Ce service permet de gérer les action d'un Département 
    /// </summary>
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<AddDepartmentResponse> AddDepartment(AddDepartmentRequest addDepartmentRequest)
        {
            var departemtDto = _mapper.Map<Department>(addDepartmentRequest);
            var newDepartment = await _departmentRepository.Insert(departemtDto);

            return _mapper.Map<AddDepartmentResponse>(newDepartment);
        }

        public async Task<bool> IsNameExists(string name)
        {
            return await _departmentRepository.IsNameExistsAsync(name);
        }
    }
}
