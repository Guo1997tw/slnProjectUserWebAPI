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

        [HttpGet("GetUser")]
        public async Task<List<UserDTO>> GetUser()
        {
            return await _userTableServices.GetUser();
        }

        [HttpGet("GetName")]
        public async Task<UserDTO> GetName(string name)
        {
            return await _userTableServices.GetName(name);
        }

        [HttpPost("CreateUser")]
        public async Task CreateUser(string name, string sex, DateTime birthday, string mobilephone)
        {
            await _userTableServices.CreateUser(name, sex, birthday, mobilephone);
        }
    }
}