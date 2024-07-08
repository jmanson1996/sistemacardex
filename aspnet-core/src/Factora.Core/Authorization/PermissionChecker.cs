using Abp.Authorization;
using Factora.Authorization.Roles;
using Factora.Authorization.Users;

namespace Factora.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
