using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;
namespace WebApplication1.Pages.Admin.Users
{
    [PermissionChecker(4)]

    public class CreateUserModel : PageModel
    {
        public readonly IAdminService adminService;

        public CreateUserModel(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [BindProperty]
        public CreateUser dto { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = adminService.GetAllRoles();
        }
        public IActionResult OnPost(List<int> SelectedRoles)
        {
            dto.SelectedRoles = SelectedRoles;
            adminService.CreateUserForAdmin(dto);
            return RedirectToPage("Index");
        }
    }
}
