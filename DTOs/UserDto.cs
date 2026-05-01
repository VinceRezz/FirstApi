using FirstApi.Models;

namespace FirstApi.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public List<TaskDto> Tasks { get; set; } = new();
    }
}
