
using Application.Requests.ServiceTab;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IServiceTabService : IBaseService<TabServices>
    {
      /// <summary>
      ///   
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
        public Task<Result<int>> Add(AddServiceRequest request); 
    }
}
