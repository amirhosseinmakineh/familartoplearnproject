using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.Models;

namespace WebApplication1.Pages
{
    public class CourseInformationModel : PageModel
    {
        private readonly ICourseService courseService;
        private readonly Toplearn.InfraStructure.Context.TopleaarnContext context;
        public CourseInformationModel(ICourseService courseService, Toplearn.InfraStructure.Context.TopleaarnContext context)
        {
            this.courseService = courseService;
            this.context = context;
        }
        [BindProperty]
        public CourseInformationDto dto { get; set; }
        public void OnGet(long id)
        {
            dto = courseService.GetCourseInformation(id);
        }
        public IActionResult OnGetDownloadEpisode(long epsodeId, long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var episode = context.Episods.Find(epsodeId);
                if(episode.IsFree is true)
                {
                    string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseEpisod", episode.FileName);
                    byte[] file = System.IO.File.ReadAllBytes(filepath);
                    return File(file, "application/force-download",episode.FileName);
                }
                if (courseService.CheckCourseForUserForDownload(id, User.Identity.Name))
                {
                    if (courseService.CheckPaymentForCourse(User.Identity.Name, id))
                    {
                        string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseEpisod", episode.FileName);
                        byte[] file = System.IO.File.ReadAllBytes(filepath);
                        return File(file, "application/force-download", episode.FileName);
                    }
                    else
                    {
                        TempData["NotPay"] = "جهت دانلود ویدیو باید فاکتور مربوط به این دوره را پرداخت کنید";
                    }
                }
                else
                {
                    TempData["NotCourseInOrder"] = "دوره مورد نظر شما در لیست فاکتور های شما وجود ندارد";
                }
                dto = courseService.GetCourseInformation(id);
            }
            else
            {
                TempData["NotLogin"] = "جهت دانلود لطفا لاگین کنید";
                dto = courseService.GetCourseInformation(id);
                return Page();
            }
            return Page();
        }
    }
}
