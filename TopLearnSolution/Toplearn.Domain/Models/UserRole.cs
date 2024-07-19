using System.ComponentModel.DataAnnotations.Schema;

namespace Toplearn.Domain.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        #endregion
    }
}
