using System.Threading.Tasks;
using Abp.Application.Services;
using Factora.Sessions.Dto;

namespace Factora.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
