using System.Threading.Tasks;
using Factora.Models.TokenAuth;
using Factora.Web.Controllers;
using Shouldly;
using Xunit;

namespace Factora.Web.Tests.Controllers
{
    public class HomeController_Tests: FactoraWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}