using System.ComponentModel.DataAnnotations;

namespace Toplearn.ApplicationService.Contract.Dtos
{
    public class ForgetPasswordDto
    {
        [EmailAddress(ErrorMessage = "لطفا فقط آدرس ایمیل را وارد کنید")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "لطفا کلمه عبور جدید را وارد کنید")]
        [MaxLength(12)]
        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}
