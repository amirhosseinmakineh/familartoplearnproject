using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.IService
{
    public interface ICourseService
    {
        bool AddCourse(AddCourseDto dto);
        List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetHeadCategory();
        List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetSubCategory();
        List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetTeachers();
        List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetStatusCourse();
        List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetLevelCourse();
        bool EditCourse(int id);
        List<CourseDto> GetAllCourseForAdmin();
        bool AddEpisodToCourse(AddEpisodDto dto);
        List<EpisodeDto> GetAllEpisodes();
        CourseDto GetAllEpisodeForManageCourse(long courseId);
        bool EditEpisode(long id);
        CourseInformationDto GetCourseInformation(long id);
        bool CheckCourseForUserForDownload(long courseId,string userName);
        bool CheckPaymentForCourse(string userName,long courseId);

    }
}
