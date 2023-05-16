using ProjectUser.Services.Dto;

namespace ProjectUser.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();

        Task<UserDto> GetByIdAsync(int id);

        Task CreateAsync(UserDto userServiceDto);

        Task UpdateAsync(UserDto userServiceDto);

        Task DeleteAsync(int id);
    }
}