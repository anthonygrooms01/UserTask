using System.ComponentModel.DataAnnotations;

namespace UserTaskAPI.Dtos.Task
{
    public class PutTaskDto
    {
        [Required]
        public int? UserId { get; set; }
        public string? Description { get; set; }
    }
}
