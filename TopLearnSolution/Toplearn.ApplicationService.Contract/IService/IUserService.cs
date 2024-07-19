using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.IService
{
    public interface IUserService
    {
        int Add(User dto);
        int Update(User user);
        User GetUserById(int id);
        bool IsExistEmail(string email);
        bool IsExistUserName(string userName);
        bool Login(LoginDto dto);
        bool ForgetPasswordEmail(ForgetPasswordDto dto);
        bool ForgetPassword(ForgetPasswordDto dto);
    }
}
