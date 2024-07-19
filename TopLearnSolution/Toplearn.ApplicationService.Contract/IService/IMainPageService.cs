using Toplearn.ApplicationService.Contract.Dtos;
namespace Toplearn.ApplicationService.Contract.IService
{
    public interface IMainPageService
    {
        List<MainCourseDto> GetCoursesForMainPage();
        CourseArchiveDto SearchByTitle(string courseTitle = "");
        CourseArchiveDto FilterAccordingToPriceAndOrder(CoursePriceState state,FilterCourseByOrder order);
        //void FilterAccordingToCreateCourseDate(string dateTime);
        CourseArchiveDto FilterAccordingToCategory(List<int> selectedGroups);
        CourseArchiveDto GetCourseForManageCategory(string title);
    }
}
