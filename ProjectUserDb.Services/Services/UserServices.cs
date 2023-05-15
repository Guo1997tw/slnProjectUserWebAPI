using ProjectUser.Services.Dto;
using ProjectUser.Services.Interface;
using ProjectUser.Repository.Interface;
using AutoMapper;
using ProjectUser.Repository.Models;

namespace ProjectUser.Services.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userTableRepository;

        public UserServices(IUserRepository userTableRepository)
        {
            _userTableRepository = userTableRepository;
        }

        public async Task<List<UserServiceDto>> GetUsersAsync()
        {
            var userModel = await _userTableRepository.GetListAsync();

            if (userModel.Any().Equals(false))
            {
                return new List<UserServiceDto>();
            }

            var usersList = userModel.Select(u => new UserServiceDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                UserSex = u.UserSex,
                UserBirthDay = u.UserBirthDay,
                UserMobilePhone = u.UserMobilePhone,
            }).ToList();

            return usersList;
        }

        public async Task<UserServiceDto> GetByIdAsync(int id)
        {
            var userDto = await _userTableRepository.GetByIdAsync(id);

            if (userDto is null)
            {
                return null;
            }

            return new UserServiceDto
            {
                UserId = userDto.UserId,
                UserName = userDto.UserName,
                UserSex = userDto.UserSex,
                UserBirthDay= userDto.UserBirthDay,
                UserMobilePhone= userDto.UserMobilePhone,
            };
        }

        public async Task CreateAsync(UserServiceDto userServiceDto)
        {
            //宣告定義且將Services UserServiceDto轉換為Repository UserModel
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserServiceDto?, UserModel>(); });

            //建立一個mapper實例
            var mapper = new Mapper(config);

            //在CreateAsync方法中，將Services UserServiceDto轉換為Repository UserModel
            var userModel = mapper.Map<UserServiceDto?, UserModel>(userServiceDto);

            await _userTableRepository.CreateAsync(userModel);
        }

        public async Task UpdateAsync(UserServiceDto userServiceDto)
        {
            //宣告定義且將Services UserServiceDto轉換為Repository UserModel
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserServiceDto, UserModel>(); });

            //建立一個mapper實例
            var mapper = new Mapper(config);

            //在UpdateAsync方法中，將Services UserServiceDto轉換為Services UserServiceDto
            var userModel = mapper.Map<UserServiceDto, UserModel>(userServiceDto);

            //UserServiceDto中找尋userId
            var userId = await _userTableRepository.GetByIdAsync(userServiceDto.UserId);

            //如果userId為空報錯，否則更新資料
            if (userId == null)
            {
                throw new Exception("Id data not found");
            }
            else
            {
                await _userTableRepository.UpdateAsync(userModel);
            }
        }

        public async Task<UserServiceDto?> DeleteAsync(int id)
        {
            var userId = await _userTableRepository.GetByIdAsync(id);

            if (userId == null) { throw new Exception("Id data not found"); }
            else { await _userTableRepository.DeleteAsync(id); }

            return null;
        }
    }
}