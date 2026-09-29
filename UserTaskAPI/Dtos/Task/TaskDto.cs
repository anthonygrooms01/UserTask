namespace UserTaskAPI.Dtos.Task
{
    public class TaskDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Description { get; set; }
    }
}
