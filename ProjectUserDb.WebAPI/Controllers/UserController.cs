using Microsoft.AspNetCore.Mvc;
using ProjectUser.Repository.Models;
using ProjectUser.Services.Interface;
using ProjectUser.WebAPI.Filter;
using ProjectUser.WebAPI.FluentValidation;
using ProjectUser.WebAPI.Models;

namespace ProjectUser.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost()]
        [UserValidatorAttribute(typeof(CreateUseParameterValidator))]
        public async Task<IActionResult> CreateUser([FromBody] UserModel userModl)
        {
            await _userService.CreateAsync(userModl);


            return Ok(new ResultOutputModel
            {
                Success = true,
                Message = string.Empty
            });
            
        }

        [HttpPut()]
        public async Task UpdateUser([FromBody] UserModel userModl)
        {
            await _userService.UpdateAsync(userModl);
        }

        [HttpDelete()]
        public async Task DeleteUser([FromBody] int id)
        {
            await _userService.DeleteAsync(id);
        }
    }
}