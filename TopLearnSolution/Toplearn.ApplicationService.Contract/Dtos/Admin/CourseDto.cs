using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos.Admin
{
    public class CourseDto
    {
        public long Id { get; set; }
        public string CourseTitle { get; set; }
        public double Price { get; set; }
        public string TeacherName { get; set; }
        public List<Episod> CourseEpisode { get; set; } = new List<Episod>();
        public long CountCourseEpisode { get; set; }
        public long EpisodeId { get; set; }
        public TimeSpan TotalDuration { get; set; }
    }

}
