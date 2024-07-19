using Microsoft.EntityFrameworkCore;
using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.Models;
using Toplearn.InfraStructure.Context;

namespace Toplearn.ApplicationService.Services
{
    public class AdminService : IAdminService
    {
        private readonly TopleaarnContext context;

        public AdminService(TopleaarnContext context)
        {
            this.context = context;
        }

        public void AddPermissionToRole(int roleId, List<int> permissionIdes)
        {
            foreach(var permission in permissionIdes)
            {
                context.RolePermissions.Add(new RolePermission()
                {
                    PermissionId = permission,
                    RoleId = roleId
                });
            }
            context.SaveChanges();
        }

        public int AddRole(AddRoleDto dto)
        {
            var role = new Role()
            {
                RoleTitle = dto.RoleTitle
            };
            context.Roles.Add(role);
            return context.SaveChanges();
        }

        public void AddRoleToUser(List<int> roleIdes, int userId)
        {
            foreach(var role in roleIdes)
            {
                context.UserRoles.Add(new UserRole()
                {
                    RoleId = role,
                    UserId = userId
                });
            }
            context.SaveChanges();
        }

        public bool CheckPermission(int roleId, string userName)
        
        {
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            var role = context.UserRoles.Include(x=> x.Role).Where(x=> x.RoleId == roleId && x.UserId == user.Id).Select(x=> new UserRole()
            {
                RoleId = roleId,
                UserId = user.Id,
            }).FirstOrDefault();
            if (role != null)
            {

                List<int> permissions = context.RolePermissions.Include(x => x.Role).Where(x => x.RoleId == role.RoleId).Select(x => x.PermissionId).ToList();
                if (permissions.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CreateUserForAdmin(CreateUser dto)
        {
            bool resutl = false;
            if (dto.Avatar != null)
            {
                string imagePath = "";
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", dto.Avatar.FileName);
                dto.UserAvatar = dto.Avatar.FileName;
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar.CopyTo(stream);
                }
            }
            var user = new User()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                IsActive = true,
                Avatar = dto.UserAvatar,
                Password = dto.Password,
                IsDelete = false,
                ActiceCode = Guid.NewGuid().ToString(),
                RegisterDate = DateTime.Now,
            };
            context.Users.Add(user);
            context.SaveChanges();
            var addUserRole = new UserRole();
            foreach(var role in dto.SelectedRoles)
            {
                 addUserRole = new UserRole()
                {
                    RoleId = role,
                    UserId = user.Id
                };
                context.UserRoles.Add(addUserRole);
            }
            context.SaveChanges();
            return true;
        }

        public void DeletePermission(int id)
        {
            var permission = context.Permissions.Where(x => x.PermissionId == id).FirstOrDefault();
            if (permission != null)
            {
                permission.IsDelete = true;
                context.Permissions.Update(permission);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Permission Not Found");
            }
        }

        public void DeleteRole(int id)
        {
            var role = context.Roles.Where(x => x.Id == id).FirstOrDefault();
            if (role != null)
            {
                role.IsDelete = true;
                context.Roles.Update(role);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Role Not Found");
            }
        }

        public void DeleteUser(int id)
        {
            var user = context.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                user.IsDelete = true;
                context.Users.Update(user);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

        public void EditPermissionRole(int roleId, List<int> permissionIdes)
        {
            context.RolePermissions.Where(x => x.RoleId == roleId).ToList().ForEach(x => context.RolePermissions.Remove(x));
            AddPermissionToRole(roleId,permissionIdes);
        }

        public bool EditUser(EditUserForAdminDto editUserForAdmin)
        {
            var user = context.Users.Include(x=> x.UserRoles).Where(x => x.Id == editUserForAdmin.Id).FirstOrDefault();
            if (user != null)
            {
                user.Email = editUserForAdmin.Email;
                user.Password = editUserForAdmin.Password;
                user.Avatar = editUserForAdmin.UserAvatar;
                 context.UserRoles.Where(x => x.Id == user.Id).ToList().ForEach(x => context.UserRoles.Remove(x));
                AddRoleToUser(editUserForAdmin.SelectedRoles,user.Id);
                if (editUserForAdmin.Avatar != null)
                {
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.Avatar);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUserForAdmin.Avatar.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        editUserForAdmin.Avatar.CopyTo(stream);
                    }
                    context.Users.Update(user);
                    context.SaveChanges();
                }
                return true;
            }
            else
            {
                throw new Exception("کاربری یافت نشد");
            }
        }

        public void EditUserRole(List<int> roleIdes, int userId)
        {
            var userRole = context.UserRoles.Where(x => x.UserId == userId).ToList();
            foreach(var role in userRole)
            {
                context.UserRoles.Remove(role);
            }
            AddRoleToUser(roleIdes,userId);
        }

        public CategoryDto GetAllCategory()
        {
            var head = context.Categories.Where(x=> x.ParentId == null).ToList();
            CategoryDto dto = new CategoryDto();
            foreach(var headCategory in head)
            {
                var subCategory = context.Categories.Where(x => x.ParentId == headCategory.Id).ToList();
                 dto = context.Categories.Select(x => new CategoryDto()
                {
                    HeadCategory = head,
                    SubCategory = subCategory
                }).FirstOrDefault();
            }
            return dto;
        }

        public List<Permission> GetAllPermissions()
        {
            return context.Permissions.ToList();
        }

        public List<RolesDto> GetAllRoles()
        {
            var roles = context.Roles.Select(x => new RolesDto()
            {
                Id = x.Id,
                RoleTitle = x.RoleTitle
            }).ToList();
            return roles;
        }

        public UpdateRoleDto GetRoleForEdit(int id)
        {
            var role = context.Roles.Where(x => x.Id == id).Select(x => new UpdateRoleDto()
            {
                Id = x.Id,
                RoleTitle = x.RoleTitle
            }).FirstOrDefault();
            return role;
        }

        public List<int> GetRolePermissions(int roleId)
        {
            return context.RolePermissions
                .Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToList();
        }

        public EditUserForAdminDto GetUserForEditInAdmin(int id)
        {
            var user = context.Users.Where(x => x.Id == id).Select(x => new EditUserForAdminDto()
            {
                UserAvatar = x.Avatar,
                Email = x.Email,
                UserName = x.UserName,
                Id = x.Id
            }).FirstOrDefault();
            return user;
        }

        public UsersDto GetUsersForAdmin(int pageId = 1, string email = "", string userName = "")
        {
            int pageCount = 0;
            int take = 5;
            IQueryable<User> users = context.Users;
            if (!string.IsNullOrEmpty(email))
            {
                users = context.Users.Where(x => x.Email.Contains(email));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                users = context.Users.Where(x => x.UserName.Contains(userName));
            }
            var dto = new UsersDto();
            dto.Users = context.Users.ToList();
            dto.CurrentPage = pageId;
            dto.PageCount = users.Count() / take;
            return dto;
        }

        public void UpdateRole(UpdateRoleDto dto)
        {
            var role = context.Roles.Where(x => x.Id == dto.Id).FirstOrDefault();
            if (role != null)
            {
                role.RoleTitle = dto.RoleTitle;
                context.Roles.Update(role);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("نقش مورد نظر موجود نیست");
            }
        }
    }
}
