using System.ComponentModel.DataAnnotations;

namespace UserTaskAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Birthday { get; set; }
        public List<Task> Tasks { get; set; } = new();
    }
}
