using System.ComponentModel.DataAnnotations;
using System.Security;

namespace Toplearn.Domain.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleTitle { get; set; }
        public bool IsDelete { get; set; }
        #region Relations
        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
