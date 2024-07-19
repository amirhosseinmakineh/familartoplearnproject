using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages.Admin.Episod
{
    [PermissionChecker(4)]
    [PermissionChecker(3)]
    public class EditEpisodModel : PageModel
    {
        private readonly ICourseService courseService;

        public EditEpisodModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public long Id { get; set; }
        public IActionResult OnGet(long id)
        {
            courseService.EditEpisode(id);
            return Redirect("/Admin/Episod/CreateEpisod");
        }
    }
}
