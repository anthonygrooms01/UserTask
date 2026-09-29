using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserTaskAPI.Data;
using UserTaskAPI.Dtos.Task;
using UserTaskAPI.Dtos.User;

namespace UserTaskAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController(AppDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTaskAPI.Models.Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            if (id <= 0)
                return BadRequest("The id field must be positive.");

            var task = await _context.Tasks.FindAsync(id);

            if (task is null)
            {
                return NotFound("The user does not exist.");
            }

            var taskDto = new TaskDto
            {
                Id = task.Id,
                UserId = task.UserId,
                Description = task.Description
            };

            return taskDto;
        }

        [HttpPost]
        public async Task<ActionResult<UserTaskAPI.Models.Task>> PostTask(CreateTaskDto createTaskDto)
        {
            if (createTaskDto.UserId <= 0)
                return BadRequest("The userid field must be positive.");

            var task = new UserTaskAPI.Models.Task
            {
                UserId = createTaskDto.UserId,
                Description = createTaskDto.Description
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var taskDto = new TaskDto
            {
                Id = task.Id,
                UserId = task.UserId,
                Description = task.Description
            };

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, taskDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutTask(int id, PutTaskDto putTaskDto)
        {
            if (id <= 0)
                return BadRequest("The id field must be positive.");

            var task = await _context.Tasks.FindAsync(id);

            if (task is null)
                return NotFound("The task does not exist.");

            task.Description = putTaskDto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchTask(int id, PatchTaskDto patchTaskDto)
        {
            if (id <= 0)
                return BadRequest("The id field must be positive.");

            var task = await _context.Tasks.FindAsync(id);

            if (task is null)
            {
                return NotFound("The task does not exist.");
            }

            if (patchTaskDto.Description is not null)
                task.Description = patchTaskDto.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (id <= 0)
                return BadRequest("The id field must be positive.");

            var task = await _context.Tasks.FindAsync(id);
            if (task is null)
            {
                return NotFound("The task does not exist.");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
