using ProjectUser.Repository.Models;

namespace ProjectUser.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetUserAsync();

        Task<UserModel> GetByIdAsync(int id);

        Task CreateAsync(UserModel user);

        Task UpdateAsync(UserModel user);

        Task DeleteAsync(int id);
    }
}