using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.IService;
namespace WebApplication1.Pages
{
    public class LoginUserModel : PageModel
    {
        private readonly IUserService userService;

        public LoginUserModel(IUserService userService)
        {
            this.userService = userService;
        }

        [BindProperty]
        public LoginDto dto { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                if (userService.Login(dto) is true)
                {
                    HttpContext.SignInAsync(dto.Principal, dto.Properties);
                    TempData["success"] = "لاگین با  موفقیت انجام شد ";
                    return Page();
                }
                else
                {
                    TempData["ErrorLogin"] = "اطلاعات وارد شده جهت ورود صحیح نیست";
                    return Page();
                }
            }
        }
    }
}
