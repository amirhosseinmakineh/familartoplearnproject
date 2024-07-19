using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos.Admin
{
    public class EditUserForAdminDto
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string UserAvatar { get; set; }
        public List<Role> Roles { get; set; }
        public List<int> SelectedRoles { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
