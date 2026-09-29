using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class UserController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            throw new Exception();
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUser(int id, PutUserDto user)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Birthday = user.Birthday;
            existingUser.Name = user.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchUser(int id, UserDTO newUser)
        {
            User user = await _context.Users.FindAsync(id);

            if (user==null)
            {
                return NotFound();
            }

            if (newUser.Id!=null)
                user.Id = (int)newUser.Id;
            if (newUser.Birthday != null)
                user.Birthday = (DateTime)newUser.Birthday;
            if (newUser.Name != null)
                user.Name = (string)newUser.Name;
            _context.SaveChangesAsync();
            return NoContent();
        }

        public class UserDTO
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public DateTime? Birthday { get; set; }
        }

        // DELETE: api/User/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
