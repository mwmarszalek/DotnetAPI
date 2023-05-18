using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    // create an instance of DataContextDapper as private _dapper (hence the underscore)
    DataContextDapper _dapper;
    //The below is a constructor that takes the config for appsettings.json using IConfiguration
    public UserController(IConfiguration config)
    {   
        // passing the config to the constructor of DataContextDapper gibing us access to connection string in Dapper
        _dapper = new DataContextDapper(config);
    }


    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        // calling LoadDataSingle method from DataContextDapper (assigned to _dapper)
        return _dapper.LoadDataSignle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetUsers/{testValue}")]
    // public IEnumerable<User> GetUsers()
    public string[] GetUsers(string testValue)
    {
        return new string[] {"user1","user2",testValue};
        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        // {
        //     Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // })
        // .ToArray();
    }
}
