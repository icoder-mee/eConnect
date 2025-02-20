using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers;
//{
//    [Route("api/[controller]")] // /api/users
//    [ApiController]

    [Authorize]
    public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await userRepository.GetMemberAsync();
            return Ok(users);
        }

        [HttpGet("{username}")] // /api/users/3
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
           var user = await userRepository.GetMemberAsync(username);

           if (user == null) return NotFound();
           
           return user;  

        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
         var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

         if (username == null) return BadRequest("Could not find user");

        var user = await userRepository.GetUserByUsernameAsync(username);

        if (user == null) return BadRequest("Coudnt find user");

        mapper.Map(memberUpdateDto, user);

        if (await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
        }
}

