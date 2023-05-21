using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{

    // create an instance of DataContextEF as private _entityFramework (hence the underscore)
    // and also create instance of Mapper
    DataContextEF _entityFramework;
    IMapper _mapper;
    //The below is a constructor that takes the config from appsettings.json using IConfiguration
    public UserEFController(IConfiguration config)
    {
        // assigning the configuration (connection string) to new DataContextEF object
        _entityFramework = new DataContextEF(config);
        // configure mapper
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            // first argument in CreateMap is where we are mapping FROM, second TO
            cfg.CreateMap<UserTOAddDto, User>();
        }));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        // In EF we don't need the SQL statement
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {

        User? user = _entityFramework.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();

        if (user != null)
        {
            return user;
        }
        throw new Exception("Failed to Get User");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {

        User? userDb = _entityFramework.Users
            .Where(u => u.UserId == user.UserId)
            .FirstOrDefault<User>();

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            // check if successfull for more than 0 rows then return ok
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Update User");

        }
        throw new Exception("Failed to Get User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserTOAddDto user)
    {
        // here we are assigning userDb to become user that we are passing in to AddUser method
        User userDb = _mapper.Map<User>(user);

        _entityFramework.Add(userDb);

        // check if successfull for more than one row then return ok
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }


    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {

        User? userDb = _entityFramework.Users
            // we are only taking userId not user
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();

        if (userDb != null)
        {
            _entityFramework.Users.Remove(userDb);
            // check if successfull for more than one row then return ok
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User");

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