namespace WordCount.API.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NoteText { get; set; }
        public Project Project { get; set; }
    }
}
