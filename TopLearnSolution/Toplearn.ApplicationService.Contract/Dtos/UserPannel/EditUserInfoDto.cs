using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Toplearn.ApplicationService.Contract.Dtos.UserPannel
{
    public class EditUserInfoDto
    {
        [Display(Name ="لطفا نام کاربری خود را وارد کنید")]
        public string? UserName { get; set; }
        [EmailAddress(ErrorMessage ="ایمیل وارد شده صحیح نیست")]
        [Display(Name ="ایمیل کاربری ")]
        public string? Email { get; set; }

        [Display(Name = "تصویر کاربر")]
        public string? UserAvatar { get; set; }
        public IFormFile? AvatarName { get; set; }
    }
}
