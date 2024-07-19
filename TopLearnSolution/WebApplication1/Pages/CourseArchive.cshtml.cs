using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages
{
    public class CourseArchiveModel : PageModel
    {
        private readonly IMainPageService mainPageService;

        public CourseArchiveModel(IMainPageService mainPageService)
        {
            this.mainPageService = mainPageService;
        }
        [BindProperty]
        public CourseArchiveDto dto { get; set; }
        public void OnGet(string courseTitle = "", CoursePriceState state = new CoursePriceState(), FilterCourseByOrder order = new FilterCourseByOrder(), List<int> selectedGroups = null,[FromRoute]string TiTle = null)
        
        {
            dto = mainPageService.SearchByTitle(courseTitle);
            dto=mainPageService.FilterAccordingToPriceAndOrder(state,order);
            dto = mainPageService.FilterAccordingToCategory(selectedGroups);
            //dto = mainPageService.GetCourseForManageCategory(TiTle);
            //ViewData["filterByCoursePrice"] = mainPageService.FilterAccordingToPrice(state);
        }
    }
}
