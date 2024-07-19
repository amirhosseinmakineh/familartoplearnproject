using System.ComponentModel.DataAnnotations;

namespace Toplearn.Domain.Models
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        #region Relations
        public Role Role { get; set; }
        public Permission Permission { get; set; }
        #endregion
    }
}
