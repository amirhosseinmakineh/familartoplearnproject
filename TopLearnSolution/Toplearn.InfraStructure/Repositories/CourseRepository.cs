using Toplearn.Domain.IRepoitories;
using Toplearn.Domain.Models;
using Toplearn.Domain.Repositories;
using Toplearn.InfraStructure.Context;

namespace Toplearn.InfraStructure.Repositories
{
    public class CourseRepository : BaseRepository<Course, long>, ICourseRepository
    {
        public CourseRepository(TopleaarnContext context) : base(context)
        {
        }
    }
}
