using System.ComponentModel.DataAnnotations;

namespace Toplearn.Domain.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public int WalletTypeId { get; set; }
        public int Amount { get; set; }
        public int TotalWallet { get; set; }
        public bool IsFinaly { get; set; }
        #region Relations
        public User User { get; set; }
        public WalletType WalletType { get; set; }
        #endregion
    }
}
