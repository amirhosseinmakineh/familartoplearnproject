using Toplearn.ApplicationService.Contract.Dtos.Order;

namespace Toplearn.ApplicationService.Contract.IService
{
    public interface IOrderService
    {
        bool AddOrder(AddOrderDto dto,string userName);
    }
}
