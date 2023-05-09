using ProjectUser.Repository.Models;

namespace ProjectUser.Services.Interface
{
    public interface IUserServices
    {
        Task<List<UserDTO>> GetUser();

        Task<UserDTO> GetName(string name);

        Task CreateUser(string userName, string userSex, DateTime userBirthDay, string userMobilePhone);
    }
}