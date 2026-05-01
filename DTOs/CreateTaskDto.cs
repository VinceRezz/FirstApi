using System.ComponentModel.DataAnnotations;

namespace FirstApi.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
       
    }
}
