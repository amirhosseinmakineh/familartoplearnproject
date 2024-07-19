using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages
{
    public class ForegetPasswordEmailModel : PageModel
    {
        private readonly IUserService userService;

        public ForegetPasswordEmailModel(IUserService userService)
        {
            this.userService = userService;
        }
        [BindProperty]
        public ForgetPasswordDto dto { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (userService.ForgetPassword(dto) is true)
            {
                TempData["Forget"] = "کلمه عبور شما با موفقیت بازیابی شد";
                return Page();
            }
            TempData["MistakeEmail"] = "ایمیل وارد شده اشتباه میباشد";
            return Page();
        }
    }
}
