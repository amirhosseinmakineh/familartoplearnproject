using System.ComponentModel.DataAnnotations;

namespace Toplearn.ApplicationService.Contract.Dtos
{
    public class AddUserDto
    {
        [Required(ErrorMessage ="لطفا نام کاربری خود را وارد کنید")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور کاربر ")]
        [Required(ErrorMessage ="لطفا کلمه عبور را وارد کنید")]
        [MaxLength(12)]
        [MinLength(8)]
        public string? Password { get; set; }
        [Compare("Password",ErrorMessage ="کلمات عبور با هم مغایرت دارند")]
        [Required(ErrorMessage ="لطفا تکرار کلمه عبور را وارد کنید")]
        [MaxLength(12)]
        [MinLength(8)]
        public string? RePassword { get; set; }
    }
}
