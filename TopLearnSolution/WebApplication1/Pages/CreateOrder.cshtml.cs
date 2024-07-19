using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Order;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.Models;

namespace WebApplication1.Pages
{
    public class CreateOrderModel : PageModel
    {
        private readonly IOrderService orderService;
        public CreateOrderModel(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [BindProperty]
        public AddOrderDto dto { get; set; } = new AddOrderDto();
        public IActionResult OnGet(long id)
        {
            dto.CourseId = id;
            if(!User.Identity.IsAuthenticated)
            {
                TempData["IsAuth"] = "لطفا جهت ثبت نام دوره در سایت لاگین کنید";
                return Page();
            }
            else
            {
                orderService.AddOrder(dto, User.Identity.Name);
                TempData["Success"] = "دروه مورد نظر با موفقیت ثبت نام شد ";
                return Page();
            }
        }
        public IActionResult OnPost()
        {
            return null;
        }
    }
}
