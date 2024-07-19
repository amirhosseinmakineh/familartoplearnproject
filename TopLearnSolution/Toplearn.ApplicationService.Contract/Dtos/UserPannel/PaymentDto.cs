using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos.UserPannel
{
    public class PaymentDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public string WalletTitle { get; set; }
        public int WalletId { get; set; }
    }
}
