using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages.Admin.Roles
{
    [PermissionChecker(4)]

    public class EditRoleModel : PageModel
    {
        private readonly IAdminService adminService;

        public EditRoleModel(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [BindProperty]
        public UpdateRoleDto dto { get; set; }
        public void OnGet(int id)
        {
            dto = adminService.GetRoleForEdit(id);
            ViewData["Permissions"] = adminService.GetAllPermissions();
            ViewData["SelectedPermissions"] = adminService.GetRolePermissions(id);
        }
        public IActionResult OnPost(List<int> SelectedPermission)
        {
            adminService.UpdateRole(dto);
            adminService.EditPermissionRole(dto.Id,SelectedPermission);
            return RedirectToPage("GetAllRoles");
        }
    }
}
