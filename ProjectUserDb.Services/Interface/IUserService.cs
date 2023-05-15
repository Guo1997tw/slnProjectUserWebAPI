using ProjectUser.Services.Dto;

namespace ProjectUser.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserServiceDto>> GetUsersAsync();

        Task<UserServiceDto> GetByIdAsync(int id);

        Task CreateAsync(UserServiceDto userServiceDto);

        Task UpdateAsync(UserServiceDto userServiceDto);

        Task<UserServiceDto?> DeleteAsync(int id);
    }
}