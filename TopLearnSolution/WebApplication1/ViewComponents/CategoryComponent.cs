using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.ViewComponents
{
    public class CategoryComponent:ViewComponent
    {
        private readonly IAdminService adminService;

        public CategoryComponent(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dto = adminService.GetAllCategory();
            return await Task.FromResult((IViewComponentResult)View("/Pages/CategoryComponent.cshtml",dto));
        }
    }
}
