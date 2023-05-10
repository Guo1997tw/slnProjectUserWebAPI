using ProjectUser.Repository.Models;

namespace ProjectUser.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetListAsync();

        Task<UserModel> GetAsync(int id);

        Task CreateAsync(UserModel user);

        Task UpdateAsync(UserModel user);

        Task DeleteAsync(int id);
    }

   
}