using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Factora.Controllers
{
    public abstract class FactoraControllerBase: AbpController
    {
        protected FactoraControllerBase()
        {
            LocalizationSourceName = FactoraConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
