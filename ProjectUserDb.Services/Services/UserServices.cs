using ProjectUser.Repository.Models;
using ProjectUser.Repository.Interface;
using ProjectUser.Services.Interface;
using System.ComponentModel.Design;

namespace ProjectUser.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userTableRepository;

        public UserServices(IUserRepository userTableRepository)
        {
            _userTableRepository = userTableRepository;
        }

        public async Task<List<UserModel>> GetUserAsync()
        {
            var result = await _userTableRepository.GetListAsync();

            if(result != null) { return result; }
            else { throw new Exception("Data Not Found"); }
        }

        public async Task<UserModel> GetByIdAsync(int _id)
        {
            var result = await _userTableRepository.GetAsync(_id);

            if (result != null) { return result; }
            else { throw new Exception("Name Not Found"); }
        }

        public async Task CreateUser(UserModel _userModel)
        {
            await _userTableRepository.CreateAsync(_userModel);
        }

        public async Task UpdateUser(UserModel _userModl)
        {
            var getID = await _userTableRepository.GetAsync(_userModl.UserId);

            if (getID == null) { throw new Exception("Data Not Found"); }
            else
            {
                getID.UserName = _userModl.UserName;
                getID.UserSex = _userModl.UserSex;
                getID.UserBirthDay = _userModl.UserBirthDay;
                getID.UserMobilePhone = _userModl.UserMobilePhone;

                await _userTableRepository.UpdateAsync(getID);
            }
        }

        public async Task DeleteUser(int _id)
        {
            var getID = await _userTableRepository.GetAsync(_id);

            if (getID == null) { throw new Exception("Data Not Found"); }
            else { await _userTableRepository.DeleteAsync(_id); }
        }
    }
}