namespace Toplearn.ApplicationService.Contract.Dtos.UserPannel
{
    public class UserInfoDto
    {
        public DateTime RegisterDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ImageName { get; set; }
        public int Wallet { get; set; } = 0;
    }
}
