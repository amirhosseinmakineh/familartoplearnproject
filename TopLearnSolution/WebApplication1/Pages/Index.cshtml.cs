using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.ApplicationService.Services;
using ZarinpalSandbox;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMainPageService mainPageService;
        public IndexModel(ILogger<IndexModel> logger, IMainPageService mainPageService)
        {
            _logger = logger;
            this.mainPageService = mainPageService;
        }
        [BindProperty]
        public MainCourseDto dto { get; set; }
        public void OnGet()
        {
            //dto = mainPageService.GetCoursesForMainPage();
        }
    }
}