using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.Models;
using Toplearn.InfraStructure.Context;
using Toplearn.Domain.IRepoitories;
using Toplearn.ApplicationService.Contract;
using System.Drawing;

namespace Toplearn.ApplicationService.Services
{
    public class CourseService : ICourseService
    {
        private readonly TopleaarnContext context;
        private readonly ICourseRepository courseRepository;

        public CourseService(TopleaarnContext context, ICourseRepository courseRepository)
        {
            this.context = context;
            this.courseRepository = courseRepository;
        }

        public bool AddCourse(AddCourseDto dto)
        {
            var course = new Course();
            if (dto.UploadIamge != null && dto.UploadIamge.IsImage())
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseImage", dto.UploadIamge.FileName);
                dto.CourseAvatar = dto.UploadIamge.FileName;
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.UploadIamge.CopyTo(stream);
                }
            }
            if (dto.FileDemoUpload != null)
            {
                string demoFileNamePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/DemoFile", dto.FileDemoUpload.FileName);
                dto.DemoFileName = dto.FileDemoUpload.FileName;
                using (var stream = new FileStream(demoFileNamePath, FileMode.Create))
                {
                    dto.FileDemoUpload.CopyTo(stream);
                }
            }
            {
                course = new Course()
                {
                    CourseAvatar = dto.CourseAvatar,
                    LevelId = dto.CourseLevelId,
                    StatusId = dto.CourseStatusId,
                    CreateDate = DateTime.Now,
                    DemoFileName = dto.DemoFileName,
                    Description = dto.Description,
                    IsDelet = false,
                    Price = dto.Price,
                    TeacherId = dto.TeacherId,
                    Title = dto.Title,
                    UpdateDate = DateTime.Now,
                    SubCategoryId = dto.SubCategoryId,
                    CategoryId =dto.CategoryId,
                };
                courseRepository.Add(course);
                courseRepository.SaveChanges();
                return true;
            }
        }

        public bool AddEpisodToCourse(AddEpisodDto dto)
        {
            var course = courseRepository.GetById(dto.CourseId);
            if (course != null)
            {
                var episodPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseEpisod",dto.UploadEpisod.FileName);
                dto.FileName = dto.UploadEpisod.FileName;
                using (var stream = new FileStream(episodPath, FileMode.Create))
                {
                    dto.UploadEpisod.CopyTo(stream);
                }
                var episod = new Episod()
                {
                    CourseId = course.Id,
                    FileName = dto.FileName,
                    IsDelet = false,
                    Time = dto.Time,
                    IsFree = false,
                    Title = dto.Title
                };
                context.Episods.Add(episod);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EditCourse(int id)
        {
            var course = courseRepository.GetById(id);
            if (course != null)
            {
                course.IsDelet = true;
                context.Courses.Update(course);
                context.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Course Not Found");
            }
        }

        public List<CourseDto> GetAllCourseForAdmin()
        {
            var episodes = context.Episods.ToList();
            var course = context.Courses.Include(x => x.User).Include(x => x.Episods).Select(x => new CourseDto()
            {
                CountCourseEpisode = x.Episods.Where(x => x.IsDelet == false).Count(),
                CourseTitle = x.Title,
                Id = x.Id,
                Price = x.Price,
                TeacherName = x.User.UserName,
            }).ToList();
            foreach(var episode in course)
            {
                var courseEpisode = episodes.Where(x => x.CourseId == episode.Id).ToList();
                episode.TotalDuration = new TimeSpan(courseEpisode.Where(x=> x.CourseId == episode.Id).Sum(x => x.Time.Ticks));
            }
            return course;
        }

        public List<EpisodeDto> GetAllEpisodes()
        {
            var epsiodes = context.Episods.Include(x=> x.Course).Select(x => new EpisodeDto()
            {
                EpisodeId = x.Id,
                Course = new Course()
                {
                    Title = x.Course.Title,
                },
                EpisodeTitle = x.Title,
                IsFree = x.IsFree,
            }).ToList();
            return epsiodes;
        }

        public List<SelectListItem> GetHeadCategory()
        {
            var head =  context.Categories.Where(x => x.ParentId == null).Select(x => new SelectListItem()
            {
                Text = x.TiTle,
                Value = x.Id.ToString(),
            }).ToList();
            return head;
        }

        public List<SelectListItem> GetLevelCourse()
        {
            var levels = context.Levels.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
            return levels;
        }

        public List<SelectListItem> GetStatusCourse()
        {
            var status = context.Statuses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
            return status;
        }
        public List<SelectListItem> GetSubCategory()
        {
            var head = context.Categories.Where(x => x.ParentId == null).ToList();
            var subCategory = new List<SelectListItem>();
            foreach(var headCategory in head)
            {
                subCategory= context.Categories.Where(x => x.ParentId == headCategory.Id).Select(x => new SelectListItem()
                {
                    Text = x.TiTle,
                    Value = x.Id.ToString()
                }).ToList();
            }
            return subCategory;
        }

        public List<SelectListItem> GetTeachers()
        
        {
            var user = context.UserRoles.Where(x => x.RoleId == 2).Include(x => x.User).Select(x => new SelectListItem()
            {
                Text = x.User.UserName,
                Value = x.User.Id.ToString()
            }).ToList();
            return user;
        }
        public CourseDto GetAllEpisodeForManageCourse(long courseId)
        {
            var episodes = context.Episods.Include(x=> x.Course).Where(x => x.CourseId == courseId).Where(x=> x.IsDelet == false).ToList();
            var courseEpisode = context.Episods.Where(x=> x.CourseId == courseId).Select(x => new CourseDto()
            {
                EpisodeId = x.Id,
                Id = x.Id,
                CountCourseEpisode = episodes.Count,
                CourseEpisode = episodes,
            }).FirstOrDefault();
            TimeSpan duration = TimeSpan.Zero;
            for(int i = 0; i < courseEpisode.CourseEpisode.Count(); i++)
            {
                duration += courseEpisode.CourseEpisode[i].Time;
            }
            courseEpisode.TotalDuration = duration;
            return courseEpisode;
        }

        public bool EditEpisode(long id)
        {
            var episode = context.Episods.Find(id);
            if (episode != null)
            {
                episode.IsDelet = true;
                context.Episods.Update(episode);
                context.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Course Not Found");
            }
        }

        public CourseInformationDto GetCourseInformation(long id)
        {
            var episodes = context.Episods.Where(x=> x.CourseId == id).ToList();
            var course = new CourseInformationDto();
                 course = context.Courses.Include(x => x.CourseLevel).Include(s => s.CourseStatus).Include(e => e.Episods).Include(u => u.User).Where(x => x.Id == id).Select(x => new CourseInformationDto()
                {
                     CourseId = x.Id,
                    DemoEpisode = x.DemoFileName,
                    LevelName = x.CourseLevel.Title,
                    Title = x.Title,
                    ImageName = x.CourseAvatar,
                    StatusName = x.CourseStatus.Title,
                    Price = x.Price,
                    TeacherName = x.User.UserName,
                    LastedUpdate = x.CreateDate,
                    Description = x.Description,
                    Episodes = episodes
                }).FirstOrDefault();
            return course;
        }

        public bool CheckCourseForUserForDownload(long courseId,string userName)
        {
            bool check = false;
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            var userOrder = context.Orders.Where(x => x.UserId == user.Id).ToList();
            var course = context.Courses.Where(x => x.Id == courseId).FirstOrDefault();
            foreach(var order in userOrder)
            {
                 check = context.Orders.Any(x => x.CourseId == course.Id);
            }
            if(check is true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckPaymentForCourse(string userName,long courseId)
        {
            var user = context.Users.FirstOrDefault(x=> x.UserName == userName);
            var course = context.Courses.Where(x => x.Id == courseId).FirstOrDefault();
            var order = context.Orders.Where(x => x.CourseId == courseId).FirstOrDefault();
            if(order.IsFinaly is true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    }
