namespace WordGoal.API.Models
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int WordGoalDaily { get; set; }
        public int WordGoalTotal { get; set; }
        public DateTimeOffset DueDate { get; set; }
    }
}
