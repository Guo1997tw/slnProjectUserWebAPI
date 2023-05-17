using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectUser.Services.Dto;
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

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _userService.GetUsersAsync();

            var response = new
            {
                Data = result,
                Output = new ResultOutputModel
                {
                    Success = true,
                    Message = string.Empty
                }
            };

            return Ok(response);
        }

        [HttpGet]
        [RequestExceptionFilter]
        [ExceptionFilter]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _userService.GetByIdAsync(id);

            var response = new
            {
                Data = result,
                Output = new ResultOutputModel
                {
                    Success = true,
                    Message = string.Empty
                }
            };

            return Ok(response);
        }

        [HttpPost]
        [RequestValidator(typeof(CreateUseParameterValidator))]
        [RequestExceptionFilter]
        [ExceptionFilter]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserParameter createUserParameter)
        {
            UserDto userDto = new()
            {
                UserName = createUserParameter.UserName,
                Gender = createUserParameter.UserSex,
                UserBirthDay = createUserParameter.UserBirthDay,
                UserMobilePhone = createUserParameter.UserMobilePhone
            };

            await _userService.CreateAsync(userDto);
            
            var result = userDto;
            
            var response = new
            {
                Data = result,
                Output = new ResultOutputModel
                {
                    Success = true,
                    Message = string.Empty
                }
            };

            return Ok(response);
        }

        [HttpPatch]
        [RequestValidator(typeof(UpdateUseParameterValidator))]
        [RequestExceptionFilter]
        [ExceptionFilter]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserParameter updateUserParameter)
        {
            UserDto userDto = new()
            {
                UserId = updateUserParameter.UserId,
                UserName = updateUserParameter.UserName,
                Gender = updateUserParameter.UserSex,
                UserBirthDay = updateUserParameter.UserBirthDay,
                UserMobilePhone = updateUserParameter.UserMobilePhone
            };

            await _userService.UpdateAsync(userDto);

            var result = userDto;

            var response = new
            {
                Data = result,
                Output = new ResultOutputModel
                {
                    Success = true,
                    Message = string.Empty
                }
            };

            return Ok(response);
        }

        [HttpDelete]
        [RequestExceptionFilter]
        [ExceptionFilter]
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