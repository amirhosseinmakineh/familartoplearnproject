using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;
namespace WebApplication1.Pages.Admin.Course
{
    [PermissionChecker(4)]
    [PermissionChecker(3)]
    public class CreateCourseModel : PageModel
    {
        private readonly ICourseService courseService;

        public CreateCourseModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public AddCourseDto dto { get; set; }
        public void OnGet()

        {
            var headCategory = courseService.GetHeadCategory();
            ViewData["HeadCategory"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(headCategory, "Value", "Text");
            var suubCategory = courseService.GetSubCategory();
            ViewData["SubCategory"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(suubCategory, "Value", "Text");
            var teachers = courseService.GetTeachers();
            ViewData["Teachers"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(teachers, "Value", "Text");
            var statuses = courseService.GetStatusCourse();
            ViewData["Statues"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(statuses, "Value", "Text");
            var levels = courseService.GetLevelCourse();
            ViewData["Levels"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(levels, "Value", "Text");
        }
        public IActionResult OnPost()
        {
            courseService.AddCourse(dto);
            return RedirectToPage("AdminIndex");
        }
    }
}
