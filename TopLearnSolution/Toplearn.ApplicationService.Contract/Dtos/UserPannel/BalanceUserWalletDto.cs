using System.ComponentModel.DataAnnotations;

namespace Toplearn.ApplicationService.Contract.Dtos.UserPannel
{
    public class BalanceUserWalletDto
    {
        public int UserId { get; set; }
        public int WalletTypeId { get; set; }
        [Required(ErrorMessage ="لطفا موضوع پرداخت خود را مشخص کنید")]        
        public string WalletTitle { get; set; }
        public int Amount { get; set; }
        public int TotalWallet { get; set; }
    }
}
