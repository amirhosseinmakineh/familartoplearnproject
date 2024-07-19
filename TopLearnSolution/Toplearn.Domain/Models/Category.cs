using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toplearn.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string TiTle { get; set; }
        public int? ParentId { get; set; }
        #region Relations
        [ForeignKey("ParentId")]
        public List<Category> SubCategory { get; set; }
        #endregion
    }
}
