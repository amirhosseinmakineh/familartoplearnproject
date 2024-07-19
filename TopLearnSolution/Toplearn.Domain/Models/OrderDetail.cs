using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toplearn.Domain.Models
{
    public class OrderDetail : BaseEntity<long>
    {
        [Required]
        public long OrderId { get; set; }
        [Required]
        public double OrderSum { get; set; }
        #region Relations
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        #endregion
    }
}
