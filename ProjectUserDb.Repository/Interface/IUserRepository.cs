using ProjectUser.Repository.Models;

namespace ProjectUser.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetListAsync();

        Task<UserModel> GetAsync(int _id);

        Task CreateAsync(UserModel _userModl);

        Task UpdateAsync(UserModel _userModl);

        Task DeleteAsync(int _id);
    }

    public enum Gender : int

    {
        M,
        F
    }
}