using System.Threading.Tasks;
using Abp.Application.Services;
using Factora.Authorization.Accounts.Dto;

namespace Factora.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
