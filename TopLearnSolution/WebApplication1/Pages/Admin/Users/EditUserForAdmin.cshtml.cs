using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;
namespace WebApplication1.Pages.Admin.Users
{
    [PermissionChecker(4)]

    public class EditUserForAdminModel : PageModel
    {
        private readonly IAdminService adminService;
        public EditUserForAdminModel(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [BindProperty]
        public EditUserForAdminDto dto { get; set; }
        public void OnGet(int id)
        {
             dto = adminService.GetUserForEditInAdmin(id);
        }
        public IActionResult OnPost(List<int> SelectedRoles)
        {
            dto.SelectedRoles = SelectedRoles;
            adminService.EditUser(dto);
            return RedirectToPage("Index");
            
        }
    }
}
