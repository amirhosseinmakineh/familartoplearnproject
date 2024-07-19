using Toplearn.ApplicationService.Contract.Dtos.UserPannel;
using Toplearn.Domain.Models;
using static Toplearn.ApplicationService.Contract.Dtos.UserPannel.UserInfoForChangePasswordDto;

namespace Toplearn.ApplicationService.Contract.IService
{
    public interface IUserPannelService
    {
        UserInfoDto GetUserInfo(string userName);
        User GetUserByUserName(string userName);
        SideBarUserPannleDto GetSideBarUserInfo(string userName);
        EditUserInfoDto GetUserInfoForEdit(string userName);
        bool EditUserProfile(string userName,EditUserInfoDto dto);
        UserInfoForChangePasswordDto GetUserInfoForChangePassword(string userName);
        bool ChangePassword(string userName,UserInfoForChangePasswordDto dto);
        int BalanceWalletUser(string userName);
        List<BalanceUserWalletDto> GetUserWallet(string userName);
        List<BalanceUserWalletDto> GetUserWallet(string userName,int amount);
        bool ChargeWallet(string userName,BalanceUserWalletDto dto);
        PaymentDto GetUserWalletForPayment(string userName);
        ShowOrderDto GetUserOrder(string userName, ShowOrderDto? order);
        bool CheckWalletForOrder(string userName);
        bool CheckUserWalletAndOrder(string userName);

    }
}
