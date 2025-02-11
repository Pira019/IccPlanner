using Domain.Entities;

namespace Infrastructure.Configurations.Interface
{
    public interface ITokenProvider
    {
        public string Create(User user, ICollection<string> userRolesName);
    }
}
