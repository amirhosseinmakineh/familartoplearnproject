using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Toplearn.ApplicationService.Contract.Dtos
{
    public class LoginDto
    {
        public string? UserName { get; set; }
        [EmailAddress(ErrorMessage ="لطفا فقط آدرس ایمیل را وارد کنید")]
        [Required(ErrorMessage ="لطفا ایمیل را وارد کنید")]
        public string Email { get; set; }
        [Required(ErrorMessage ="لطفا کلمه عبور را وارد کنید")]
        [MaxLength(12)]
        [MinLength(8)]
        public string Password { get; set; }
        public ClaimsPrincipal? Principal { get; set; }
        public AuthenticationProperties? Properties { get; set; }
    }
}
