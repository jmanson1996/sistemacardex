using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Factora.Configuration.Dto;

namespace Factora.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : FactoraAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
