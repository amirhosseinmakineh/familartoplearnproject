using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages.Admin.Episod
{
    [PermissionChecker(4)]
    [PermissionChecker(3)]
    public class EpisodesForManageCourseModel : PageModel
    {
        private readonly ICourseService courseService;

        public EpisodesForManageCourseModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public CourseDto dto { get; set; }
        public void OnGet(long id)
        {
            dto = courseService.GetAllEpisodeForManageCourse(id);
        }
    }
}
