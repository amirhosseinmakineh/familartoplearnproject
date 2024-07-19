using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos
{
    public class CourseInformationDto
    {
        private string title;

        public long CourseId { get; set; }
        public string Title { get => title; set => title = value; }
        public string Description { get; set;}
        public double Price { get; set; }
        public string TeacherName { get; set; }
        public string StatusName { get; set; }
        public string LevelName { get; set; }
        public TimeSpan CourseDuration { get; set; }
        public DateTime LastedUpdate { get; set; }
        public string ImageName { get; set; }
        public string DemoEpisode { get; set; }
        public List<Episod> Episodes { get; set; }
    }
    public class CourseEpisodeDto
    {
        public List<Course> Course { get; set; }
        public List<Episod> Episods { get; set; }
    }
}
