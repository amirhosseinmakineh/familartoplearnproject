using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Attributes;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;

namespace WebApplication1.Pages.Admin.Episod
{
    [PermissionChecker(4)]
    [PermissionChecker(3)]
    public class IndexModel : PageModel
    {
        private readonly ICourseService courseService;

        public IndexModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public List<EpisodeDto> dto { get; set; }
        public void OnGet()
        {
            dto = courseService.GetAllEpisodes();
        }
    }
}
