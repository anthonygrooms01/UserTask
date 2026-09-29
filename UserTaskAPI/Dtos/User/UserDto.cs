namespace UserTaskAPI.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
