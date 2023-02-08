using System.ComponentModel.DataAnnotations;

namespace WordGoal.API.Models
{
    public class ProjectForCreationDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
