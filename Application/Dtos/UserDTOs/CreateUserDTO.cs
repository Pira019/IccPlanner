
using Domain.Entities;

namespace Application.Dtos.UserDTOs
{
    public class CreateUserDTO
    {
        public string Password { get; set; }
        public Member Member { get; set; }
    }
}
