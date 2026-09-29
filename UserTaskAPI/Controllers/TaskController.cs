using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserTaskAPI.Data;

namespace UserTaskAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController(AppDbContext _context) : ControllerBase
    {
        // GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTaskAPI.Models.Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Task/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserTaskAPI.Models.Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Task/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutTask(int id, UserTaskAPI.Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Task
        [HttpPost]
        public async Task<ActionResult<UserTaskAPI.Models.Task>> PostTask(UserTaskAPI.Models.Task task)
        {
            
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchTask(int id, TaskDTO newTask)
        {
            UserTaskAPI.Models.Task task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            if (newTask.Id != null)
                task.Id = (int)newTask.Id;
            if (newTask.UserID != null)
                task.UserId = (int)newTask.UserID;
            if (newTask.Description != null)
                task.Description = (string)newTask.Description;
            _context.SaveChangesAsync();
            return NoContent();
        }

        public class TaskDTO
        {
            public int? Id { get; set; }
            public int? UserID { get; set; }
            public string? Description { get; set; }
        }

        // DELETE: api/User/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
