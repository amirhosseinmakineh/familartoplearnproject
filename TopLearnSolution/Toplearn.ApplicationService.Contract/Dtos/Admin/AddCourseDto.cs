using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos.Admin
{
    public class AddCourseDto
    {
        public int TeacherId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CourseAvatar { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string DemoFileName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CourseLevelId { get; set; }
        public int CourseStatusId { get; set; }

        public IFormFile UploadIamge { get; set; }
        public IFormFile FileDemoUpload { get; set; }
    }

}
