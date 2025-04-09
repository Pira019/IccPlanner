using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Configurations.Interface
{
    public interface ITokenProvider
    {
        public string Create(User user, ICollection<string> userRolesName);
        public void AddJwtToCookie(HttpResponse httpResponse, string token);
    }
}
