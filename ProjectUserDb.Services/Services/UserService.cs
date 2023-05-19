using AutoMapper;
using ProjectUser.Services.Dto;
using ProjectUser.Repository.Models;
using ProjectUser.Services.Interface;
using ProjectUser.Repository.Interface;
using ProjectUser.Services.Exceptions;
using ProjectUser.Common.Extensions;
using ProjectUser.Services.Mapping;

namespace ProjectUser.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userTableRepository;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userTableRepository, IMapper mapper)
        {
            _userTableRepository = userTableRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var userModels = await _userTableRepository.GetListAsync();

            if (userModels.BeEmpty())
            {
                return new List<UserDto>();
            }

            var user = userModels.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Gender = u.UserSex,
                UserBirthDay = u.UserBirthDay,
                UserMobilePhone = u.UserMobilePhone,
            }).ToList();

            return user;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userTableRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new UserNotFoundException(id);
            }

            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Gender = user.UserSex,
                UserBirthDay = user.UserBirthDay,
                UserMobilePhone = user.UserMobilePhone,
            };
        }

        public async Task CreateAsync(UserDto userServiceDto)
        {
            if (userServiceDto is null)
            {
                throw new ArgumentNullException(nameof(userServiceDto));
            }

            var user = this._mapper.Map<UserDto, UserModel>(userServiceDto);

            await _userTableRepository.CreateAsync(user);
        }

        public async Task UpdateAsync(UserDto userServiceDto)
        {
            if (userServiceDto is null)
            {
                throw new ArgumentNullException(nameof(userServiceDto));
            }

            var alreadyExist = await _userTableRepository.ExistAsync(userServiceDto.UserId);

            if (alreadyExist.Equals(false))
            {
                throw new UserNotFoundException(userServiceDto.UserId);
            }

            var user = this._mapper.Map<UserDto, UserModel>(userServiceDto);

            await _userTableRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userTableRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new UserNotFoundException(id);
            }

            await _userTableRepository.DeleteAsync(id);
        }
    }
}