using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toplearn.Domain.Models
{
    public class Episod:BaseEntity<long>
    {
        public long CourseId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
        [Required]
        public bool IsFree { get; set; } = false;
        #region Relations
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        #endregion
    }
}
