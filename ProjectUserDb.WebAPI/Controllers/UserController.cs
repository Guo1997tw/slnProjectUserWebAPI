using Microsoft.AspNetCore.Mvc;
using ProjectUser.Repository.Models;
using ProjectUser.Services.Interface;
using ProjectUser.WebAPI.Filter;
using ProjectUser.WebAPI.FluentValidation;
using ProjectUser.WebAPI.Models;

namespace ProjectUser.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userServices)
        {
            _userService = userServices;
        }

        [HttpGet]
        public async Task<List<UserModel>> GetList()
        {
            return await _userService.GetUserAsync();
        }

        [HttpGet]
        public async Task<UserModel> GetId(int id)
        {
            return await _userService.GetByIdAsync(id);
        }

        [HttpPost]
        [UserValidatorAttribute(typeof(CreateUseParameterValidator))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserParameter createUserParameter)
        {
            UserModel userModel = new UserModel()
            {
                UserName = createUserParameter.UserName,
                UserSex = createUserParameter.UserSex,
                UserBirthDay = createUserParameter.UserBirthDay,
                UserMobilePhone = createUserParameter.UserMobilePhone
            };
            await _userService.CreateAsync(userModel);

            return Ok(new ResultOutputModel
            {
                Success = true,
                Message = string.Empty
            });
        }

        [HttpPatch()]
        [UserValidatorAttribute(typeof(UpdateUseParameterValidator))]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserParameter updateUserParameter)
        {
            UserModel userModel = new UserModel()
            {
                UserId = updateUserParameter.UserId,
                UserName = updateUserParameter.UserName,
                UserSex = updateUserParameter.UserSex,
                UserBirthDay = updateUserParameter.UserBirthDay,
                UserMobilePhone = updateUserParameter.UserMobilePhone
            };

            await _userService.UpdateAsync(userModel);

            return Ok(new ResultOutputModel
            {
                Success = true,
                Message = string.Empty
            });
        }

        [HttpDelete()]
        public async Task DeleteUser([FromBody] int id)
        {
            await _userService.DeleteAsync(id);
        }
    }
}