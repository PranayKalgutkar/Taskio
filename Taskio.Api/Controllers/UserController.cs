using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taskio.App.DTOs;
using Taskio.App.IServices;

namespace Taskio.Api.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetUsers()
        // {
        //     var users = await _userService.GetUsersAsync();
        //     return Ok(users);
        // }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignupDto dto)
        {
            //int num1 = 10, num2 = 0;

            //int result = num1 / num2;

            var result = await _userService.SignUp(dto);
            return Ok(result); // `ResponseFilter` will wrap this automatically

            // var result = await _userService.SignUp(dto);
            // if (result.Message != null)
            // {
            //     return Ok(new { result.Message });
            //     //return Ok(result);
            // }
            // //return Ok(new { message = "User signed up successfully." });
            // return Ok(result);
        }
    }
}

/*
Taskio.Api
    Controllers
       UserController.cs
    Middlewares
Taskio.App
    DTOs
        SignUpDto.cs
    IRepository
        IUserRepository.cs
    IServices
        IUserServices.cs
Taskio.Domain
    Entities
        User.cs
        ExceptionLog.cs
Taskio.Infra
    Services
        UserService.cs
Taskio.Persist
    Repositories
        UserRepository
    AppDbContext.cs
Taskio.Tests
    Services
        UserServiceTests.cs
Taskio.sln
*/