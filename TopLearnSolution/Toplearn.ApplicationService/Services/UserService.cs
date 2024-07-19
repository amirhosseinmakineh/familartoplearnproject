using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.Models;
using Toplearn.InfraStructure.Context;

namespace Toplearn.ApplicationService.Services
{
    public class UserService : IUserService
    {
        private readonly TopleaarnContext context;

        public UserService(TopleaarnContext context)
        {
            this.context = context;
        }

        public int Add(User dto)
        {
            context.Users.Add(dto);
            return context.SaveChanges();
        }

        public bool ForgetPassword(ForgetPasswordDto dto)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (user != null)
            {
                user.Password = dto.NewPassword;
                Update(user);
                return true;
            }
            return false;
        }

        public bool ForgetPasswordEmail(ForgetPasswordDto dto)
        {
            var user = context.Users.Where(x => x.Email == dto.Email).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetUserById(int id)
        {
            var user = context.Users.Find(id);
            return user;
        }
        public bool IsExistEmail(string email)
        {
            var IsExistEmail = context.Users.Any(x => x.Email == email);
            return IsExistEmail;
        }

        public bool IsExistUserName(string userName)
        {
            var user = context.Users.Any(x => x.UserName == userName);
            return user;
        }

        public bool Login(LoginDto dto)
        {
            var loginuser = context.Users.Where(x => x.Email == dto.Email && x.Password == dto.Password && x.IsActive == false).FirstOrDefault();
            if (loginuser != null)
            {
                var claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,loginuser.UserName),
                    new Claim("UserId",loginuser.Id.ToString())
                };
                var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                dto.Principal = new ClaimsPrincipal(identity);
                dto.Properties = new AuthenticationProperties();
                return true;
            }
            return false;
        }

        public int Update(User user)
        {
            context.Users.Update(user);
            return context.SaveChanges();
        }
    }
}
