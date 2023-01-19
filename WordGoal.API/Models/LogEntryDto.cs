namespace WordGoal.API.Models
{
    public class LogEntryDto
    {
        public int Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public int WordCount { get; set; }
        public int NumberOfMinutes { get; set; }
    }
}
