using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos
{
    public class CategoryDto
    {
        public List<Category> HeadCategory { get; set; }
        public List<Category> SubCategory { get; set; }
        public List<Course> Courses { get; set; }
    }
}
