using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages.Admin.Episod
{
    [PermissionChecker(4)]
    [PermissionChecker(3)]
    public class CreateEpisodModel : PageModel
    {
        private readonly ICourseService courseService;

        public CreateEpisodModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public AddEpisodDto dto { get; set; } = new AddEpisodDto();
        public void OnGet(long id)
        {
            dto.CourseId = id;
        }
        public IActionResult OnPost()
        {
            courseService.AddEpisodToCourse(dto);
            return Redirect("/Admin/Course/Index");
        }
    }
}
