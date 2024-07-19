using System.ComponentModel.DataAnnotations;

namespace Toplearn.ApplicationService.Contract.Dtos.Order
{
    public class AddOrderDto
    {
        public long OrderId { get; set; }
        [Required]
        public long CourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        public double Price { get; set; }
        [Required]
        public string CourseTitle { get; set; }
        public bool IsFinaly { get; set; }
    }
}
