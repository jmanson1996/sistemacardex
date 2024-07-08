using System.Threading.Tasks;
using Factora.Configuration.Dto;

namespace Factora.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
