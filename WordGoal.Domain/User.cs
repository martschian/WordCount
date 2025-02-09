﻿namespace WordGoal.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Note> Notes { get; set; } = new List<Note>(); 
    }
}
