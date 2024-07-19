using System.ComponentModel.DataAnnotations;

namespace Toplearn.ApplicationService.Contract.Dtos.Admin
{
    public class UpdateRoleDto
    {
        public int Id { get; set; }
        [Required]
        public string RoleTitle { get; set; }
    }
}
