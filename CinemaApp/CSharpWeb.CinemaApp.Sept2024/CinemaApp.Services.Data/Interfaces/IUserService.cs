using CinemaApp.Web.ViewModels.Admin.UserManagement;

namespace CinemaApp.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();
        Task<bool> UserExistByIdAsync(Guid userId);
        Task<bool> AssignUserToRoleAsync(Guid userId, string roleName);
        Task<bool> RemoveUserRoleAsync(Guid userId, string roleName);

        Task<bool> DeleteUserAsync(Guid userId);
    }
}
