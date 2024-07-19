using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class LogOutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.SignOutAsync();
        }
    }
}
