using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages.Admin.Roles
{
    [PermissionChecker(4)]

    public class GetAllRolesModel : PageModel
    {
        private readonly IAdminService adminService;
        public GetAllRolesModel(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [BindProperty]
        public List<RolesDto> dtos { get; set; }
        public void OnGet()
        {
            dtos = adminService.GetAllRoles();
        }
    }
}
