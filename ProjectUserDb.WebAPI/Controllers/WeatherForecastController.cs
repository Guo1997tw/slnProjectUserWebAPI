using Microsoft.AspNetCore.Mvc;
using ProjectUser.Repository.Models;
using ProjectUser.Services.Interface;

namespace ProjectUser.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        //private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserServices _userTableServices;

        public WeatherForecastController(/*ILogger<WeatherForecastController> logger,*/ IUserServices userTableServices)
        {
            //_logger = logger;
            _userTableServices = userTableServices;
        }

        /*[HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/

        [HttpGet("GetList")]
        public async Task<List<UserModel>> GetList()
        {
            return await _userTableServices.GetUserAsync();
        }

        [HttpGet("GetId")]
        public async Task<UserModel> GetId(int _id)
        {
            return await _userTableServices.GetByIdAsync(_id);
        }

        [HttpPost("CreateUser")]
        public async Task CreateUser([FromQuery] UserModel _userModl)
        {
            await _userTableServices.CreateUser(_userModl);
        }

        [HttpPut("UpdateUser")]
        public async Task UpdateUser([FromQuery] UserModel _userModl)
        {
            await _userTableServices.UpdateUser(_userModl);
        }

        [HttpDelete("DeleteUser")]
        public async Task DeleteUser([FromQuery]int _id)
        {
            await _userTableServices.DeleteUser(_id);
        }
    }
}