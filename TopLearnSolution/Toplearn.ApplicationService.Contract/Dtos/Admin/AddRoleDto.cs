using System.ComponentModel.DataAnnotations;

namespace Toplearn.ApplicationService.Contract.Dtos.Admin
{
    public class AddRoleDto
    {
        [Required]
        public string RoleTitle { get; set; }
    }
}
