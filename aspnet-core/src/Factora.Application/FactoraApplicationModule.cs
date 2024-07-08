using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Factora.Authorization;

namespace Factora
{
    [DependsOn(
        typeof(FactoraCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class FactoraApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<FactoraAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(FactoraApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
