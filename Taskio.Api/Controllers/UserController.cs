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
            await _userService.SignUp(dto);
            return Ok(new { message = "User signed up successfully." });
        }
    }
}