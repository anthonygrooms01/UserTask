using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserTaskAPI.Data;
using UserTaskAPI.Dtos.User;
using UserTaskAPI.Models;

namespace UserTaskAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(AppDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            if (id <= 0)
                return BadRequest("The id field must be positive.");

            var user = await _context.Users.FindAsync(id);

            if (user is null)
            {
                return NotFound("The user does not exist.");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Birthday = user.Birthday
            };

            return userDto;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(CreateUserDto createUserDto)
        {
            if (createUserDto.Name is null)
                return BadRequest("The name field is required.");

            var user = new User()
            {
                Name = createUserDto.Name,
                Birthday = createUserDto.Birthday,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Birthday = user.Birthday,
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUser(int id, PutUserDto putUserDto)
        {
            if (id <= 0)
                return BadRequest("The id field must be positive.");

            var user = await _context.Users.FindAsync(id);

            if (user is null)
            {
                return NotFound("The user does not exist.");
            }

            if (putUserDto.Name is null)
            {
                return BadRequest("The name field is required.");
            }

            user.Birthday = putUserDto.Birthday;
            user.Name = putUserDto.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchUser(int id, PatchUserDto patchUserDto)
        {
            if (id <= 0)
                return BadRequest("The id field must be positive.");

            var user = await _context.Users.FindAsync(id);

            if (user is null)
            {
                return NotFound("The user does not exist.");
            }

            if (patchUserDto.Birthday is not null)
                user.Birthday = patchUserDto.Birthday;
            if (patchUserDto.Name is not null)
                user.Name = patchUserDto.Name;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
                return BadRequest("The id field must be positive.");

            var user = await _context.Users.FindAsync(id);

            if (user is null)
            {
                return NotFound("The user does not exist.");
            }

            if (user.Tasks.Count > 0)
                return Conflict("This user has tasks. Please reassign.");

                _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
