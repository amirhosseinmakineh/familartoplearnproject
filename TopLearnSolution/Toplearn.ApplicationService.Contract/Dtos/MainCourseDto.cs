using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos
{
    public class MainCourseDto
    {
        public long Id { get; set; }
        public string CourseTitle { get; set; }
        public double CoursePrice { get; set; }
        public string TeacherName { get; set; }
        public string CourseImage { get; set; }
        public TimeSpan CourseDuration { get; set; }
    }
}
