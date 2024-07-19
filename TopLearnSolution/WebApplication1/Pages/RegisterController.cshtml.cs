using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Contract.Convertor;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.Models;

namespace WebApplication1.Pages
{
    public class RegisterControllerModel : PageModel
    {
        private readonly IUserService service;

        public RegisterControllerModel(IUserService service)
        {
            this.service = service;
        }
        [BindProperty]
        public AddUserDto dto { get; set; }
        public void OnGet()
        
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (service.IsExistUserName(dto.UserName) is false)
            {
                if (service.IsExistEmail(dto.Email) is false)
                {
                    var user = new User()
                    {
                        Email = dto.Email,
                        Password = dto.Password,
                        ActiceCode = Guid.NewGuid().ToString(),
                        Avatar = "Defult.jpg",
                        IsActive = false,
                        IsDelete = false,
                        RegisterDate = DateTime.Now,
                        UserName = dto.UserName
                    };
                    service.Add(user);
                    return RedirectToPage("SuccessRegister", new User()
                    {
                        UserName = user.UserName
                    });
                }
                else
                {
                    TempData["IsExistEmail"] = "ایمیل وارد شده موجود میباشد";
                    return Page();
                }
            }
            else
            {
                TempData["IsExistUserName"] = "نام کاربری  وارد شده موجود میباشد";
                return Page();
            }
        }
    }
}
