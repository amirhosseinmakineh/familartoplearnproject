using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos.Admin
{
    public class CreateUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserAvatar { get; set; }
        public IFormFile Avatar { get; set; }
       public List<RolesDto> Roles { get; set; }
        public List<int> SelectedRoles { get; set; }
    }
}
