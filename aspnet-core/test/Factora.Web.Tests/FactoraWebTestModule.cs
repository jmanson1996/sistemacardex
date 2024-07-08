using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Factora.EntityFrameworkCore;
using Factora.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Factora.Web.Tests
{
    [DependsOn(
        typeof(FactoraWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class FactoraWebTestModule : AbpModule
    {
        public FactoraWebTestModule(FactoraEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FactoraWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(FactoraWebMvcModule).Assembly);
        }
    }
}