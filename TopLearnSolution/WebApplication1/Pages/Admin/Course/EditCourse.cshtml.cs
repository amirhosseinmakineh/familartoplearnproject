using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages.Admin.Course
{
    [PermissionChecker(4)]
    [PermissionChecker(3)]
    public class EditCourseModel : PageModel
    {
        private readonly ICourseService courseService;

        public EditCourseModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        public IActionResult OnGet(int id)
        {
            courseService.EditCourse(id);
            return RedirectToPage("CreateCourse");
        }
    }
}
