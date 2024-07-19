using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Toplearn.ApplicationService.Contract.Dtos.UserPannel;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.IRepoitories;
using Toplearn.Domain.Models;
using Toplearn.InfraStructure.Context;

namespace Toplearn.ApplicationService.Services
{
    public class UserPannelService : IUserPannelService
    {
        private readonly TopleaarnContext context;
        private readonly IUserService userService;
        public readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        public UserPannelService(TopleaarnContext context, IUserService userService, IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository)
        {
            this.context = context;
            this.userService = userService;
            this.orderDetailRepository = orderDetailRepository;
            this.orderRepository = orderRepository;
        }

        public UserInfoDto GetUserInfo(string userName)
        {
            var user = context.Users.Where(x => x.UserName == userName).Select(x => new UserInfoDto()
            {
                UserName = x.UserName,
                Email = x.Email,
                RegisterDate = x.RegisterDate,
                Wallet = x.Walet
            }).FirstOrDefault();
            return user;
        }
        public User GetUserByUserName(string userName)
        {
            var user = context.Users.FirstOrDefault(x => x.UserName == userName);
            return user;
        }

        public SideBarUserPannleDto GetSideBarUserInfo(string userName)
        {
            var user = context.Users.Where(x => x.UserName == userName).Select(x => new SideBarUserPannleDto()
            {
                ImageName = x.Avatar,
                RegisterUserDate = x.RegisterDate,
                UserName = x.UserName
            }).FirstOrDefault();
            return user;
        }

        public EditUserInfoDto GetUserInfoForEdit(string userName)
        {
            var user = context.Users.Where(x => x.UserName == userName).Select(x => new EditUserInfoDto()
            {
                UserName = x.UserName,
                Email = x.Email,
                UserAvatar = x.Avatar
            }).FirstOrDefault();
            return user;
        }

        public bool EditUserProfile(string userName, EditUserInfoDto dto)
        {
            bool result = false;
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.Avatar);
                if (dto.AvatarName != null)
                {
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/UserAvatar/{dto.AvatarName.FileName}");
                    dto.UserAvatar = dto.AvatarName.FileName;
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        dto.AvatarName.CopyTo(stream);
                    }
                }
                user.UserName = dto.UserName;
                user.Avatar = dto.UserAvatar;
                user.Email = dto.Email;
                userService.Update(user);
                context.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public UserInfoForChangePasswordDto GetUserInfoForChangePassword(string userName)
        {
            var user = context.Users.Where(x => x.UserName == userName).Select(x => new UserInfoForChangePasswordDto()
            {
                OldPassword = x.Password
            }).FirstOrDefault();
            if (user is null)
                throw new Exception("کاربری یافت نشد");
            return user;
        }

        public bool ChangePassword(string userName, UserInfoForChangePasswordDto dto)
        {
            var user = context.Users.Where(x => x.UserName == userName && x.Password == dto.OldPassword).FirstOrDefault();
            if (user != null)
            {
                user.Password = dto.NewPassword;
                userService.Update(user);
                context.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("کلمه عبور قبلی شما صحیح نیست");
            }

        }

        public int BalanceWalletUser(string userName)
        {
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var enter = context.Wallets.Where(x => x.UserId == user.Id && x.WalletTypeId == 1).Select(x => x.Amount
                ).ToList();
                var exit = context.Wallets.Where(x => x.UserId == user.Id && x.WalletTypeId == 2).Select(x => x.Amount).ToList();
                var sum = (enter.Sum() - exit.Sum());
                return sum;
            }
            else
            {
                throw new Exception("کاربری یافت نشد");
            }
        }
        public List<BalanceUserWalletDto> GetUserWallet(string userName)
        {
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var userWallet = context.Wallets.Where(x => x.UserId == user.Id).Select(x => new BalanceUserWalletDto()
                {
                    Amount = x.Amount,
                    UserId = user.Id,
                    WalletTitle = x.Title,
                    WalletTypeId = x.WalletTypeId,
                    TotalWallet = x.TotalWallet
                }).ToList();
                return userWallet;
            }
            else
            {
                throw new Exception("کاربری یافت نشد");
            }
        }
        public List<BalanceUserWalletDto> GetUserWallet(string userName, int amount = 0)
        {
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var userWallet = context.Wallets.Where(x => x.UserId == user.Id).Select(x => new BalanceUserWalletDto()
                {
                    Amount = amount,
                    UserId = user.Id,
                    WalletTitle = x.Title,
                    WalletTypeId = x.WalletTypeId,
                    TotalWallet = x.TotalWallet
                }).ToList();
                return userWallet;
            }
            else
            {
                throw new Exception("کاربری یافت نشد");
            }
        }
        public bool ChargeWallet(string userName, BalanceUserWalletDto dto)
        {
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var wallet = new Wallet()
                {
                    UserId = user.Id,
                    Amount = dto.Amount,
                    Title = "شارژ کیف پول ",
                    WalletTypeId = 1,
                };
                context.Wallets.Add(wallet);
                context.SaveChanges();
                var sumWallet = context.Wallets.Where(x => x.UserId == user.Id).ToList();
                var totatl = 0;
                if (sumWallet.Count == 0)
                {
                    totatl = wallet.Amount;
                    wallet.TotalWallet = totatl;
                }
                else
                {
                    var falseWallet = context.Wallets.Where(x => x.UserId == user.Id && x.IsFinaly == false).ToList();
                    for (int i = 0; i < falseWallet.Count(); i++)
                    {
                        totatl += falseWallet[i].Amount;
                    }
                    wallet.TotalWallet = totatl;
                    wallet.IsFinaly = true;
                    context.Wallets.Update(wallet);
                    context.SaveChanges();
                }
                return true;
            }
            else
            {
                throw new Exception("کاربری یافت نشد");
            }
        }

        public PaymentDto GetUserWalletForPayment(string userName)
        {
            var userId = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            var wallet = context.Wallets.Where(x => x.UserId == userId.Id).Include(x => x.User).Select(x => new PaymentDto()
            {
                WalletId = x.Id,
                Email = x.User.Email,
                Amount = x.Amount,
                UserName = x.User.UserName,
                WalletTitle = x.Title
            }).FirstOrDefault();
            return wallet;
        }

        public ShowOrderDto GetUserOrder(string userName, ShowOrderDto? order)
        {
            var orders = orderRepository.GetAll();
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            order = context.Orders.Where(x => x.UserId == user.Id).Include(x => x.OrderDetails).Select(x => new ShowOrderDto()
            {
                Orders = orders.Where(x => x.UserId == user.Id).ToList(),
            }).FirstOrDefault();
            int sum = 0;
            if (order != null)
            {
                if (order.Orders != null)
                {
                    var orderNotFinaly = order.Orders.Where(x => x.IsFinaly is false).ToList();
                    for (int i = 0; i < orderNotFinaly.Count(); i++)
                    {
                        int price = (int)orderNotFinaly[i].Price;
                        sum += price;
                    }
                }
                order.OrderSum = sum;
            }
            return order;
        }

        public bool CheckWalletForOrder(string userName)
        {
            bool result = false;
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                int sum = 0;
                var userWallet = context.Wallets.Include(x => x.User).Where(x => x.UserId == user.Id).ToList();
                for (int i = 0; i < userWallet.Count; i++)
                {
                    sum = userWallet[i].TotalWallet;
                }
                double orderSum = 0;
                var userOrder = context.Orders.Where(x => x.UserId == user.Id).ToList();
                for (int i = 0; i < userOrder.Count; i++)
                {
                    orderSum = userOrder[i].Price;
                    var order = context.Orders.Where(x => x.Price == userOrder[i].Price && x.IsFinaly == false).ToList();
                    if (order.Count > 0)
                    {
                        if (sum >= userOrder[i].Price)
                        {
                            sum -= (int)userOrder[i].Price;
                            var orderFinally = order.Where(x => x.Price == userOrder[i].Price).FirstOrDefault();
                            orderFinally.IsFinaly = true;
                            context.Orders.Update(orderFinally);
                            context.SaveChanges();
                            var wallet = context.Wallets.Where(x => x.UserId == user.Id).ToList();
                            //foreach (var item in wallet)
                            //{
                            //item.IsFinaly = true;
                            //item.TotalWallet = sum;
                            //item.WalletTypeId = 2;
                            //item.Amount += (int)userOrder[i].Price;
                            //item.Title = "برداشت از یکف پول";
                            //item.UserId = user.Id;
                            //context.Wallets.Add(item);
                            //}
                            var addwallet = new Wallet()
                            {
                                Amount = (int)orderSum,
                                IsFinaly = true,
                                TotalWallet = sum,
                                Title = "برداشت از کیف پول ",
                                UserId = user.Id,
                                WalletTypeId = 2
                            };
                            context.Wallets.Add(addwallet);
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public bool CheckUserWalletAndOrder(string userName)
        {
            bool result = false;
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var arrayWallet = context.Wallets.Where(x => x.UserId == user.Id).ToArray();
                var userWallet = new Wallet();
                for(int i = 1; i < arrayWallet.Count(); i++)
                {
                    userWallet = arrayWallet[i];
                }
                var userOrder = context.Orders.Where(x => x.UserId == user.Id && x.IsFinaly == false).ToList();
                double orderSum = 0;
                for (int i = 0; i < userOrder.Count; i++)
                {
                    orderSum = userOrder[i].Price;
                }
                if(userWallet.TotalWallet < orderSum)
                {
                    result =  false;
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
