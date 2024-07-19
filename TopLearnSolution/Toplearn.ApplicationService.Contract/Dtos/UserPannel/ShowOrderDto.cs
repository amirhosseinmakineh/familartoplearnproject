using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos.UserPannel
{
    public class ShowOrderDto
    {
        public List<Toplearn.Domain.Models.Order>? Orders { get; set; } = new List<Domain.Models.Order>();
        public OrderDetail OrderDetail { get; set; }
        public int OrderSum { get; set; }
        public double UserWallet { get; set; }

    }
}
