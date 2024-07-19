using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.Domain.IRepoitories;
using Toplearn.Domain.Models;
using Toplearn.Domain.Repositories;
using Toplearn.InfraStructure.Context;

namespace Toplearn.InfraStructure.Repositories
{
    public class OrderRepository : BaseRepository<Order, long>, IOrderRepository
    {
        public OrderRepository(TopleaarnContext context) : base(context)
        {
        }
    }
}
