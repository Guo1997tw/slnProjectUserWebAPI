using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectUser.Repository.Models;
using ProjectUser.Services.Interface;
using ProjectUser.WebAPI.Filter;
using ProjectUser.WebAPI.FluentValidation;
using ProjectUser.WebAPI.Models;

namespace ProjectUser.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userServices)
        {
            _userService = userServices;
        }

        [HttpPost()]
        [RequestValidator(typeof(CreateUseParameterValidator))]
        public async Task<IActionResult> CreateUser([FromBody] UserModel _userModl)
        {

            await _userService.CreateAsync(_userModl);


            return Ok(new ResultOutputModel 
            {
                Success = true,
                Message = string.Empty
            });


            
        }
    }
}
