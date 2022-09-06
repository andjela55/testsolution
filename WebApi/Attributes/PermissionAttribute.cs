using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text.Json;

namespace WebApp.Attributes
{
    public class PermissionAtribute : TypeFilterAttribute
    {
        public PermissionAtribute(params int[] permissions) : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { permissions.ToList()};
        }
    }
    public class PermissionFilter : IAuthorizationFilter
    {
        List<int> _requiredPermissions;
        public PermissionFilter(List<int>requiredPermissions)
        {
            _requiredPermissions = requiredPermissions;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                context.Result = new ForbidResult();
                return;
            }
            
            var claims=identity.Claims;

            var permissions = JsonSerializer.Deserialize<List<int>>(claims.FirstOrDefault(x=>x.Type=="Permissions")?.Value);

            if (permissions == null)
            {
                context.Result = new ForbidResult();
                return;

            }
            if (_requiredPermissions.Any(x => permissions.Any(y => y == x))){
                return;
            }

            context.Result = new UnauthorizedResult();
            return;


        }
    }

}
