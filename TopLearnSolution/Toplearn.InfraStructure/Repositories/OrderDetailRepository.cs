using Toplearn.Domain.IRepoitories;
using Toplearn.Domain.Models;
using Toplearn.Domain.Repositories;
using Toplearn.InfraStructure.Context;

namespace Toplearn.InfraStructure.Repositories
{
    public class OrderDetailRepository : BaseRepository<OrderDetail, long>, IOrderDetailRepository
    {
        public OrderDetailRepository(TopleaarnContext context) : base(context)
        {
        }
    }
}
