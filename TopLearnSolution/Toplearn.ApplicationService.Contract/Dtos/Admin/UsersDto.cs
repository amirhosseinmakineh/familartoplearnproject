using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos.Admin
{
    public class UsersDto
    {
        public List<User> Users { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
