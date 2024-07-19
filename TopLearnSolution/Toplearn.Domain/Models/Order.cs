using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toplearn.Domain.Models
{
    public class Order:BaseEntity<long>
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Required]
        public int UserId { get; set; }
        public long CourseId { get; set; }
        public double Price { get; set; }
        [Required]
        public string CourseTitle { get; set; }
        public bool IsFinaly { get; set; }
        #region Relations
        public ICollection<OrderDetail> OrderDetails { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("CourseId")]
        public Course Courses { get; set; }
        #endregion
    }
}
