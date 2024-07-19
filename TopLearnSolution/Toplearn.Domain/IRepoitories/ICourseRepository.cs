using Toplearn.Domain.Models;
using Toplearn.InfraStructure.IRepoitories;

namespace Toplearn.Domain.IRepoitories
{
    public interface ICourseRepository:IBaseRepository<Course,long>
    {
    }
}
