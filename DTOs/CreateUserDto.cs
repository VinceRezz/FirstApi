using FirstApi.Models;
using System.ComponentModel.DataAnnotations;

namespace FirstApi.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}
