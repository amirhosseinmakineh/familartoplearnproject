using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toplearn.Domain.Models
{
    public class WalletType
    {
        [Key, DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        #region Relations
        public List<Wallet> Wallets { get; set; }
        #endregion
    }
}
