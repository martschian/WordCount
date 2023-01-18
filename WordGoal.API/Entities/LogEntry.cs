namespace WordGoal.API.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public int WordCount { get; set; }
        public int NumberOfMinutes { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
