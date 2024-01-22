using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return Ok(user);
        }
        
        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return Ok(user);
        }

    }
}
