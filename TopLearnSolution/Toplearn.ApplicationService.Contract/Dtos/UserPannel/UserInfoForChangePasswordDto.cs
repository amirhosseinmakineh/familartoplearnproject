using System.ComponentModel.DataAnnotations;

namespace Toplearn.ApplicationService.Contract.Dtos.UserPannel
{
    public partial class UserInfoForChangePasswordDto
    {
        [Required(ErrorMessage = "لطفا کلمه عبور قبلی خود را وارد کنید")]
        [MaxLength(11, ErrorMessage = "کلمه عبور قبلی بیشتر از 11 کاراکتر نمیتواند باشد")]
        [MinLength(8, ErrorMessage = "کلمه عبور قبلی کمتر از 8 کاراکتر نمیتواند باشد")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "لطفا کلمه عبور جدید خود را وارد کنید")]
        [MaxLength(11, ErrorMessage = "کلمه عبور جدید بیشتر از 11 کاراکتر نمیتواند باشد")]
        [MinLength(8, ErrorMessage = "کلمه عبور جدید کمتر از 8 کاراکتر نمیتواند باشد")]
        public string NewPassword { get; set; }
    }
}
