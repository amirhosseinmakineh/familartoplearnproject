using Microsoft.EntityFrameworkCore;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.IRepoitories;
using Toplearn.Domain.Models;
using Toplearn.InfraStructure.Context;

namespace Toplearn.ApplicationService.Services
{
    public class MainPageService : IMainPageService
    {
        private readonly TopleaarnContext context;
        private readonly ICourseRepository courseRepository;
        public MainPageService(TopleaarnContext context, ICourseRepository courseRepository)
        {
            this.context = context;
            this.courseRepository = courseRepository;
        }

        public CourseArchiveDto FilterAccordingToCategory(List<int> selectedGroups)
        {
            var courses = courseRepository.GetAll();
            var category = context.Categories.Where(x => x.ParentId == null);
            IQueryable<Category> subCategory = null;

                foreach(var headCategory in category)
                {
                    subCategory = context.Categories.Where(x => x.ParentId == headCategory.Id);
                if (selectedGroups.Count > 0)
                {
                    courses = courses.Where(x => x.CategoryId == headCategory.Id);
                        foreach(var sub in subCategory)
                        {
                            courses = courses.Where(x => x.CategoryId == sub.Id);
                        }
                }
            }
            var dto = new CourseArchiveDto()
            {
                Courses = courses.ToList(),
                HeadCategory = category.ToList(),
                SubCategory = subCategory.ToList()
            };
            return dto;
        }

        public CourseArchiveDto FilterAccordingToPrice(CoursePriceState state)
        {
            var courses = courseRepository.GetAll();
            return null;
        }

        public CourseArchiveDto FilterAccordingToPriceAndOrder(CoursePriceState state, FilterCourseByOrder order)
        {
            var course = courseRepository.GetAll();
            if (state == CoursePriceState.all)
            {
                course.ToList();
            }
            else if (state == CoursePriceState.buy)
            {
                course = course.Where(x => x.Price > 0);
            }
            else if (state == CoursePriceState.free)
            {
                course = course.Where(x => x.Price < 0 && x.Price == 0);
            }
            if (order == FilterCourseByOrder.dateTime && state == CoursePriceState.free)
            {
                course = course.OrderBy(x => x.CreateDate).Where(x => x.Price == 0 && x.Price < 0);
            }
            else if(order == FilterCourseByOrder.dateTime && state == CoursePriceState.buy)
            {
               course =  course.OrderBy(x => x.CreateDate).Where(x => x.Price > 0);
            }
            else if(order == FilterCourseByOrder.dateTime && state == CoursePriceState.all)
            {
               course = course.OrderBy(x => x.CreateDate); 
            }
            if (order == FilterCourseByOrder.title && state == CoursePriceState.free)
            {
                course = course.OrderBy(x => x.Title).Where(x => x.Price == 0 && x.Price < 0);
            }
            else if (order == FilterCourseByOrder.title && state == CoursePriceState.buy)
            {
                course = course.OrderBy(x => x.CreateDate).Where(x => x.Price > 0);
            }
            else if(order == FilterCourseByOrder.title && state == CoursePriceState.all)
            {
                course = course.OrderBy(x => x.CreateDate);
            }
            if (order == FilterCourseByOrder.Price && state == CoursePriceState.free)
            {
                course = course.OrderBy(x => x.Title).Where(x => x.Price == 0 && x.Price < 0);
            }
            else if (order == FilterCourseByOrder.Price && state == CoursePriceState.buy)
            {
                course = course.OrderBy(x => x.CreateDate).Where(x => x.Price > 0);
            }
            else if(order == FilterCourseByOrder.Price && state == CoursePriceState.all)
            {
                course = course.OrderBy(x => x.CreateDate);
            }
            var dto = new CourseArchiveDto()
            {
                Courses = course.ToList()
            };
            return dto;
        }

        public CourseArchiveDto GetCourseForManageCategory(string title)
        {
            var courses = courseRepository.GetAll();
            var category = context.Categories.Where(x => x.TiTle == title).ToList();
            foreach(var item in category)
            {
                courses = courses.Where(x => x.CategoryId == item.Id);
            }
            var dto = new CourseArchiveDto()
            {
                Courses = courses.ToList(),
            };
            return dto;
        }

        public List<MainCourseDto> GetCoursesForMainPage()
        
        
        {
            MainCourseDto dtos = new MainCourseDto();
            var courses = context.Courses.Include(x => x.Episods).Include(x => x.User).ThenInclude(x => x.UserRoles.Where(x => x.RoleId == 3)).Where(c => c.IsDelet == false).ToList();
            var dto = courses.Select(x => new MainCourseDto()
            {
                CourseDuration = new TimeSpan(x.Episods.Sum(x => x.Time.Ticks)),
                CourseImage = x.CourseAvatar,
                CoursePrice = x.Price,
                CourseTitle = x.Title,
                TeacherName = x.User.UserName,
                Id = x.Id
            }).ToList();
            return dto;
        }

        public CourseArchiveDto SearchByTitle(string courseTitle = "")
        {
            //var category = context.Categories.ToList();
            var course = courseRepository.GetAll();
            //if (selectedGroups.Count !=0)
            //{
            //    foreach (var item in category.Where(x => x.ParentId == null))
            //    {
            //        course = course.Where(x => x.CategoryId == item.Id);
            //        if (item.SubCategory.Any())
            //        {
            //            foreach (var sub in category.Where(x => x.ParentId == item.Id))
            //            {
            //                course = course.Where(x => x.CategoryId == sub.Id);
            //            }
            //        }
            //    }
            //}
            if (!string.IsNullOrEmpty(courseTitle))
            {
                course = course.Where(x => x.Title.Contains(courseTitle));
            }

            var dto = new CourseArchiveDto()
            {
                Courses = course.Include(x=> x.User).ToList(),
            };
            return dto;
        }
    }
}
