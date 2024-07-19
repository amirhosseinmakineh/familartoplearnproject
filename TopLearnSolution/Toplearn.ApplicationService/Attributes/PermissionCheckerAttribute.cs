using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.ApplicationService.Services;
using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Attributes
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private  IAdminService adminService;
        public int roleId;
        public PermissionCheckerAttribute(int roleId)
        {
            this.roleId = roleId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
             adminService = (IAdminService)context.HttpContext.RequestServices.GetService(typeof(IAdminService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = context.HttpContext.User.Identity.Name;
                adminService.CheckPermission(roleId,user);
            }
            else
            {
                context.Result = new RedirectResult("/LoginUser");
            }
        }
    }
}
