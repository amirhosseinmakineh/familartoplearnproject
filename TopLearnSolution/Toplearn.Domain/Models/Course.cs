using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.Domain.Models
{
    public class Course:BaseEntity<long>
    {
        public Course()
        {
            Episods = new HashSet<Episod>();
        }
        public int TeacherId { get; set; }
        public int StatusId { get; set; }
        public int LevelId { get; set; }
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CourseAvatar { get; set; }
        [Required]
        public double Price  { get; set; }
        [Required]
        public string DemoFileName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        #region Relations
        [ForeignKey("TeacherId")]
        public User User { get; set; }
        [ForeignKey("CategoryId")]
        public Category HeadCategory { get; set; }
        [ForeignKey("SubCategoryId")]
        public Category SubCategory { get; set; }
        public ICollection<Episod> Episods { get; set; }
        public Level CourseLevel { get; set; }
        public Status CourseStatus { get; set; }
        #endregion
    }
}
