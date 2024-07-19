using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.UserPannel;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.Models;
using ZarinpalSandbox;

namespace WebApplication1.Areas.UserPannel.Controllers
{
    [Area("UserPannel")]
    [PermissionChecker(3)]
    public class HomeController : Controller
    {
        private readonly IUserPannelService userPannelService;
        private readonly IHttpContextFactory httpContextFactory;
        public HomeController(IUserPannelService userPannelService, IHttpContextFactory httpContextFactory)
        {
            this.userPannelService = userPannelService;
            this.httpContextFactory = httpContextFactory;
        }
        [Route("UserPannel/index")]
        [HttpGet]
        public IActionResult Index()
        {
            var userInfo = userPannelService.GetUserInfo(User.Identity.Name);
            return View(userInfo);
        }
        [Route("UserPannel/Edit")]
        [HttpGet]
        public IActionResult Edit()
        {
            var user = userPannelService.GetUserInfoForEdit(User.Identity.Name);
            return View(user);
        }
        [Route("UserPannel/Edit")]
        [HttpPost]
        public IActionResult Edit(EditUserInfoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            else
            {
                userPannelService.EditUserProfile(User.Identity.Name, dto);
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Redirect("/LoginUser");

            }
        }
        [Route("UserPannel/ChangePassword")]
        [HttpGet]
        public IActionResult ChangeUserPannelPassword()
        {
            var userInfo = userPannelService.GetUserInfoForChangePassword(User.Identity.Name);
            return View(userInfo);
        }
        [Route("UserPannel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangeUserPannelPassword(UserInfoForChangePasswordDto dto)
        {
            userPannelService.ChangePassword(User.Identity.Name, dto);
            return RedirectToAction("Index");
        }
        [Route("UserPannel/UserWallet")]
        [HttpGet]
        public IActionResult UserWallet()
        {
            ViewBag.userWallet = userPannelService.GetUserWallet(User.Identity.Name);
            return View();
        }
        [Route("UserPannel/UserWallet")]
        [HttpPost("UserPannel/UserWallet/{amount}")]
        public IActionResult UserWallet(BalanceUserWalletDto dto, int amount)
        {
            amount = dto.Amount;
            //userPannelService.ChargeWallet(User.Identity.Name, dto);
            return Redirect($"/UserPannel/Payment/{amount}");
        }
        [Route("UserPannel/Payment/{amount}")]
        [HttpGet]
        public IActionResult Payment(int amount)
        {
            var userWallet = userPannelService.GetUserWalletForPayment(User.Identity.Name);
            if (userWallet != null)
            {
                var payment = new Payment(amount);
                var request = payment.PaymentRequest(userWallet.WalletTitle, $"http://localhost:5044/UserPannel/OnlinePayment/" + amount, "amirhosseoin@gmail.com", "09389596838");
                if (request.Result.Status == 100)
                {
                    string userEmail = userWallet.Email;
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + request.Result.Authority);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                var payment = new Payment(amount);
                    var request = payment.PaymentRequest($"پرداخت برای عدم موجودی کافی برای فاکتور", $"http://localhost:5044/UserPannel/OnlinePayment/" + amount, "amirhosseoin@gmail.com", "09389596838");
                if (request.Result.Status == 100)
                {
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + request.Result.Authority);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [Route("UserPannel/OnlinePayment/{amount}")]
        [HttpGet]
        public IActionResult OnlinePayment(int amount,BalanceUserWalletDto dto)
        {
            var user = userPannelService.GetUserWalletForPayment(User.Identity.Name);
            if (HttpContext.Request.Query["Status"] != "" && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var payment = new Payment(amount);
                var request = payment.Verification(authority).Result;
                if (request.Status == 100)
                {
                    userPannelService.ChargeWallet(User.Identity.Name,dto);
                    //userPannelService.CheckWalletForOrder(User.Identity.Name);
                    return Redirect("/UserPannel/UserWallet");
                }
                return BadRequest();
            }
            TempData["NotFound"] = "پرداخت شما با خطا مواجه شد";
            return View();
        }
        [Route("UserPannel/ShowOrder")]
        [HttpGet]
        public IActionResult ShowOrder()
        {
            var order = userPannelService.GetUserOrder(User.Identity.Name,null);
            if (userPannelService.CheckUserWalletAndOrder(User.Identity.Name) is false)
            {
                TempData["IsCheck"] = "hello";
            }
            if(order == null)
            {
                order = new ShowOrderDto();
                return View(order);
            }
            else
            {
                return View(order);
            }
        }
        [Route("UserPannel/ShowOrder/{ordersum}")]
        [HttpGet]
        public IActionResult ShowOrder(int ordersum)
        {
            if(userPannelService.CheckWalletForOrder(User.Identity.Name) is false)
            {
                return Redirect($"/UserPannel/Payment/{ordersum}");
            }
            else
            {
                //userPannelService.CheckWalletForOrder(User.Identity.Name);
                var order = userPannelService.GetUserOrder(User.Identity.Name,null);
                TempData["Success"] = "فاکتور شما با موفقیت پرداخت شد";
                return View(order);
            }
        }
    }
}
