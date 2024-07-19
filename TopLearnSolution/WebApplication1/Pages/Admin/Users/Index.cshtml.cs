using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.ApplicationService.Services;

namespace WebApplication1.Pages.Admin.Users
{
    [PermissionChecker(4)]
    public class IndexModel : PageModel
    {
        private readonly IAdminService adminService;

        public IndexModel(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [BindProperty]
        public UsersDto dto { get; set; }
        public void OnGet(int pageId = 1, string userName = "", string email = "")
        {
            dto = adminService.GetUsersForAdmin(1,email,userName);
        }
    }
}
