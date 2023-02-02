using System.ComponentModel.DataAnnotations;

namespace WordGoal.API.Models
{
    public class NoteForCreationDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string NoteText { get; set; } = string.Empty;
    }
}
