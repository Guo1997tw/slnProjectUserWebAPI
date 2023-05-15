using Microsoft.AspNetCore.Mvc;
using ProjectUser.Services.Interface;
using ProjectUser.WebAPI.Filter;
using ProjectUser.WebAPI.FluentValidation;
using ProjectUser.WebAPI.Models;
using ProjectUser.Services.Dto;

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
        public async Task<IActionResult> GetList()
        {
            var result = await _userService.GetUsersAsync();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _userService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        [UserValidator(typeof(CreateUseParameterValidator))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserParameter createUserParameter)
        {
            UserServiceDto userServiceDto = new UserServiceDto()
            {
                UserName = createUserParameter.UserName,
                UserSex = createUserParameter.UserSex,
                UserBirthDay = createUserParameter.UserBirthDay,
                UserMobilePhone = createUserParameter.UserMobilePhone
            };
            await _userService.CreateAsync(userServiceDto);

            return Ok(new ResultOutputModel
            {
                Success = true,
                Message = string.Empty
            });

            /*if(userServiceDto.UserName.Contains(' ').Equals(true))
            {
                throw new Exception ("Name Error");
            }*/
        }

        [HttpPatch()]
        [UserValidatorAttribute(typeof(UpdateUseParameterValidator))]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserParameter updateUserParameter)
        {
            UserServiceDto userServiceDto = new UserServiceDto()
            {
                UserId = updateUserParameter.UserId,
                UserName = updateUserParameter.UserName,
                UserSex = updateUserParameter.UserSex,
                UserBirthDay = updateUserParameter.UserBirthDay,
                UserMobilePhone = updateUserParameter.UserMobilePhone
            };

            await _userService.UpdateAsync(userServiceDto);

            return Ok(new ResultOutputModel
            {
                Success = true,
                Message = string.Empty
            });
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteUser([FromBody] int id)
        {
            await _userService.DeleteAsync(id);

                        return Ok(new ResultOutputModel
            {
                Success = true,
                Message = string.Empty
            });
        }
    }
}