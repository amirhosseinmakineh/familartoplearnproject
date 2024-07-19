using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages.Admin
{
    [PermissionChecker(4)]
    [PermissionChecker(3)]
    public class AdminIndexModel : PageModel
    {
        private readonly IAdminService adminService;

        public AdminIndexModel(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public void OnGet()
        {
            if(adminService.CheckPermission(4,User.Identity.Name) is false)
            {
                TempData["checkPermission"] = "unauthorize";

            }
        }
    }
}
