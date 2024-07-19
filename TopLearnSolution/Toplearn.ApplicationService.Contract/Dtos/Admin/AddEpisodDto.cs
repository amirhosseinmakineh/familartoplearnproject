using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Toplearn.ApplicationService.Contract.Dtos.Admin
{
    public class AddEpisodDto
    {
        public long CourseId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
        public IFormFile UploadEpisod { get; set; }
        [Required]
        public bool IsFree { get; set; } = false;
    }
}
