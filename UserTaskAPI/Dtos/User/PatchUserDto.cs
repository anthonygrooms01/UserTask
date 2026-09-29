namespace UserTaskAPI.Dtos.User
{
    public class PatchUserDto
    {
        public required string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
