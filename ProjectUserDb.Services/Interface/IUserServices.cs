using ProjectUser.Repository.Models;

namespace ProjectUser.Services.Interface
{
    public interface IUserServices
    {
        Task<List<UserModel>> GetUserAsync();

        Task<UserModel> GetByIdAsync(int _id);

        Task CreateUser(UserModel _userModel);

        Task UpdateUser(UserModel _userModl);

        Task DeleteUser(int _id);
    }
}