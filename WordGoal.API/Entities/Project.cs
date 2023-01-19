namespace WordGoal.API.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<LogEntry> LogEntries { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
