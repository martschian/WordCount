namespace WordGoal.Domain
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTimeOffset DueDate { get; set; }
        public int WordGoalDaily { get; set; }
        public int WordGoalTotal { get; set; }
        public string Description { get; set; } = string.Empty;
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<LogEntry> LogEntries { get; set; } = new List<LogEntry>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
