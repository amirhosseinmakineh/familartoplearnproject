using Microsoft.AspNetCore.Mvc;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.ViewComponents
{
    public class CourseComponent:ViewComponent
    {
        private readonly IMainPageService mainPageService;
        public CourseComponent(IMainPageService mainPageService)
        {
            this.mainPageService = mainPageService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = mainPageService.GetCoursesForMainPage();
            return await Task.FromResult((IViewComponentResult)View("/Pages/CourseComponent.cshtml", courses));
        }
    }
}
