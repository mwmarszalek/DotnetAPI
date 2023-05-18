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

    // This endpoints gets all users:
    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
            FROM TutorialAppSchema.Users";
        // here we are calling the LoadData dapper method that we created and passing in the sql string from Azure Studio
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }

    // This endpoints gets a single user (filtered by userId)
    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
            FROM TutorialAppSchema.Users
            WHERE UserId = " + userId.ToString();
        // here we are calling the LoadData dapper method that we created and passing in the sql string from Azure Studio
        User user = _dapper.LoadDataSignle<User>(sql);
        return user;

    }
}




  // return new string[] {"user1","user2"};
        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        // {
        //     Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // })
        // .ToArray();