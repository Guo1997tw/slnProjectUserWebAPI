using ProjectUser.Repository.Models;
using ProjectUser.Repository.Interface;
using ProjectUser.Services.Interface;

namespace ProjectUser.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userTableRepository;

        public UserServices(IUserRepository userTableRepository)
        {
            _userTableRepository = userTableRepository;
        }

        public async Task<List<UserDTO>> GetUser()
        {
            var result = await _userTableRepository.GetAllUserAsync();

            if(result != null) { return result; }
            else { throw new Exception("Data Not Found"); }
        }

        public async Task<UserDTO> GetName(string name)
        {
            var result = await _userTableRepository.GetDetailUserAsync(name);

            if (result != null) { return result; }
            else { throw new Exception("Name Not Found"); }
        }

        public async Task CreateUser(string userName, string userSex, DateTime userBirthDay, string userMobilePhone)
        {
            await _userTableRepository.CreateUserAsync(userName, userSex, userBirthDay, userMobilePhone);
        }
    }
}