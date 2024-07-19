using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.Models;

namespace WebApplication1.Pages.Admin.Roles
{
    [PermissionChecker(4)]
    public class CreateRoleModel : PageModel
    {
        private readonly IAdminService adminService;

        public CreateRoleModel(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [BindProperty]
        public AddRoleDto dto { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = adminService.GetAllPermissions();
        }
        public IActionResult OnPost(List<int> SelectedPermission)
        {
            int roleId = adminService.AddRole(dto);
            adminService.AddPermissionToRole(roleId, SelectedPermission);
            return RedirectToPage("GetAllRoles");
        }
    }
}
