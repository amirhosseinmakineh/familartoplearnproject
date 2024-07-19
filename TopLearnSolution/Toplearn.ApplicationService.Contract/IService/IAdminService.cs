using Toplearn.ApplicationService.Contract.Dtos;
using Toplearn.ApplicationService.Contract.Dtos.Admin;
using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.IService
{
    public interface IAdminService
    {
        UsersDto GetUsersForAdmin(int pageId = 1, string email = "", string userName = "");
        bool CreateUserForAdmin(CreateUser dto);
        EditUserForAdminDto GetUserForEditInAdmin(int id);
        bool EditUser(EditUserForAdminDto editUserForAdmin);
        List<RolesDto> GetAllRoles();
        int AddRole(AddRoleDto dto);
        void UpdateRole(UpdateRoleDto dto);
        UpdateRoleDto GetRoleForEdit(int id );
        void AddRoleToUser(List<int> roleIdes,int userId);
        void EditUserRole(List<int> roleIdes, int userId);
        void AddPermissionToRole(int roleId,List<int> permissionIdes);
        void EditPermissionRole(int roleId, List<int> permissionIdes);
        List<int> GetRolePermissions(int roleId);
        List<Permission> GetAllPermissions();
        void DeleteUser(int id);
        void DeleteRole(int id);
        void DeletePermission(int id);
        bool CheckPermission(int roleId,string userName);
        CategoryDto GetAllCategory();
    }
}
