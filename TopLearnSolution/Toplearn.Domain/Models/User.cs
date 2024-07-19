using System.ComponentModel.DataAnnotations;

namespace Toplearn.Domain.Models
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Courses = new HashSet<Course>();
        }
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = "نام کاربری ";
        [Display(Name = "ایمیل کاربری ")]
        [Required]
        public string Email { get; set; }
        [Display(Name ="کلمه عبور کاربر ")]
        [Required]
        [MaxLength(12)]
        [MinLength(8)]
        public string Password { get; set; }
        [Display(Name ="کاربر حذف شده یا نه ؟")]
        [Required]
        public bool IsDelete { get; set; }
        [Display(Name ="کد فعالسازی کاربر ")]
        [Required]
        public string ActiceCode { get; set; }
        [Display(Name = "کاربر فعال شده یا نه ؟")]
        [Required]
        public bool  IsActive { get; set; }
        [Display(Name ="تاریخ ثبت نام کاربر")]
        public DateTime RegisterDate { get; set; }
        [Display(Name ="عکس کاربر")]
        [Required]
        public  string Avatar { get; set; }
        [Display(Name ="کیف پول کاربر")]
        public int Walet { get; set; }
        #region Relations
        public List<UserRole> UserRoles { get; set; }
        public List<Wallet> Wallet { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Course> Courses { get; set; }
        #endregion
    }
}
