using Dapper;
using ProjectUser.Repository.Models;

namespace ProjectUser.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetAllUserAsync();

        Task<UserDTO> GetDetailUserAsync(string name);

        Task CreateUserAsync(string userName, string userSex, DateTime userBirthDay, string userMobilePhone);
    }
}