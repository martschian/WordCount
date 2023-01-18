namespace WordGoal.API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public ICollection<Project>? Projects { get; set; }
    }
}
