using Microsoft.Build.Framework;

namespace WordGoal.API.Models
{
    public class LogEntryForCreationDto
    {
        [Required]
        public int WordCount { get; set; }
        [Required]
        public int NumberOfMinutes { get; set; }
    }
}
