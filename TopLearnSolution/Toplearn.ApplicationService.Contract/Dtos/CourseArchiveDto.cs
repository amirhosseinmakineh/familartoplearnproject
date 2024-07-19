using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos
{
    public class CourseArchiveDto
    {
        public List<Course> Courses { get; set; }
        public CoursePriceState State { get; set; }
        public List<Category> HeadCategory { get; set; }
        public List<Category> SubCategory { get; set; }
        public List<int> selectedGroups { get; set; } = new List<int>();
    }
}
