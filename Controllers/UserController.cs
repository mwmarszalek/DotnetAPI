using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
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
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
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
        User user = _dapper.LoadDataSingle<User>(sql);
        return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.Users
        SET [FirstName] = '" + user.FirstName +
            "', [LastName] = '" + user.LastName +
            "', [Email] = '" + user.Email +
            "', [Gender] = '" + user.Gender +
            "', [Active] = '" + user.Active +
        "' WHERE userId = " + user.UserId;
        Console.WriteLine(sql);

        // check if ExecuteSql returns True, if not throws an error
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserTOAddDto user)
    {

        string sql = @"INSERT INTO TutorialAppSchema.Users(
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]
            ) VALUES (" +
                " '" + user.FirstName +
            "',  '" + user.LastName +
            "',  '" + user.Email +
            "',   '" + user.Gender +
            "',   '" + user.Active +
            "')";
            Console.WriteLine(sql);

         // check if ExecuteSql returns True, if not throws an error
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }


    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
        DELETE FROM TutorialAppSchema.Users 
            WHERE UserId= " + userId.ToString();
        Console.WriteLine(sql);

         // check if ExecuteSql returns True, if not throws an error
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Delete User");
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