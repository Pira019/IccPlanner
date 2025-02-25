using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Department;
using Application.Responses.Department;
using AutoMapper;
using Domain.Abstractions;
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
        private readonly IPostRepository _postRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IPostRepository postRepository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _postRepository = postRepository;    
        }
        public async Task<AddDepartmentResponse> AddDepartment(AddDepartmentRequest addDepartmentRequest)
        {
            var departemtDto = _mapper.Map<Department>(addDepartmentRequest);
            var newDepartment = await _departmentRepository.Insert(departemtDto);

            return _mapper.Map<AddDepartmentResponse>(newDepartment);
        }

        public async Task AddDepartmentResponsable(AddDepartmentRespoRequest addDepartmentRespoRequest)
        {
            var memberDepartment = await _departmentRepository.FindDepartmentMember(addDepartmentRespoRequest.MemberId, addDepartmentRespoRequest.DepartmentId);

            //Verifier s'il est deja memberDepartment
            if ( memberDepartment == null) 
            {
                var departmentMemberDto = _mapper.Map<DepartmentMember>(addDepartmentRespoRequest);
                memberDepartment = await _departmentRepository.SaveDepartmentMember(departmentMemberDto);
            }

            var post = await _postRepository.FindPosteByName(MemberPost.RESPONSABLE_REFERENT);

            var departmentMemberPost =  _mapper.Map<DepartmentMemberPost>(addDepartmentRespoRequest);
            departmentMemberPost.PosteId = post!.Id;
            departmentMemberPost.DepartmentMemberId = memberDepartment.Id;

           await _departmentRepository.SaveDepartmentMemberPost(departmentMemberPost);
        }

        public async Task<bool> IsNameExists(string name)
        {
            return await _departmentRepository.IsNameExistsAsync(name);
        }
    }
}
