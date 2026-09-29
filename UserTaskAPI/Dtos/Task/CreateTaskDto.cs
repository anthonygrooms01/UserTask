namespace UserTaskAPI.Dtos.Task
{
    public class CreateTaskDto
    {
        public int UserId { get; set; }
        public string? Description { get; set; }
    }
}
