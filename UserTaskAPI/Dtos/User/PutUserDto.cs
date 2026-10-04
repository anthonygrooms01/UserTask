using System.ComponentModel.DataAnnotations;

namespace UserTaskAPI.Dtos.User
{
    public class PutUserDto
    {
        [Required]
        public string? Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
