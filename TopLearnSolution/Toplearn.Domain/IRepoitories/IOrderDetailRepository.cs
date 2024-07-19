using Toplearn.Domain.Models;
using Toplearn.InfraStructure.IRepoitories;

namespace Toplearn.Domain.IRepoitories
{
    public interface IOrderDetailRepository :IBaseRepository<OrderDetail,long>
    {
    }
}
