using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
//{
//    [Route("api/[controller]")] // /api/users
//    [ApiController]

    [Authorize]
    public class UsersController(IUserRepository userRepository) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await userRepository.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{username}")] // /api/users/3
        public async Task<ActionResult<AppUser>> GetUser(string username)
        {
            var user = await userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }

        }
    }

