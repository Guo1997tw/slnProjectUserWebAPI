using ProjectUser.Repository.Models;

namespace ProjectUser.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserModel>> GetUserAsync();

        Task<UserModel> GetByIdAsync(int _id);

        Task CreateAsync(UserModel _userModel);

        Task UpdateAsync(UserModel _userModl);

        Task DeleteAsync(int _id);
    }
}