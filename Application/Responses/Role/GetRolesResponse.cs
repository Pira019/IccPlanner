using Application.Dtos.Role;

namespace Application.Responses.Role
{
    public class GetRolesResponse
    {
        public ICollection<GetRolesDTO>? Items { get; set; }
    }
}
