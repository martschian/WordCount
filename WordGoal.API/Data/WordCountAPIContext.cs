using Microsoft.EntityFrameworkCore;
using WordGoal.API.Entities;

namespace WordGoal.API.Data
{
    public class WordGoalAPIContext : DbContext
    {
        public WordGoalAPIContext(DbContextOptions<WordGoalAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Note { get; set; } = default!;
        
        public DbSet<Project> Project { get; set; } = default!;

        public DbSet<LogEntry> LogEntry { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Adam Handoq",
                    Email = "adam@handoq.com"
                });

            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Title = "The Great American Novel",
                    Description = "What is says on the tin, baybeeee!",
                    UserId = 1,
                });

            modelBuilder.Entity<Note>().HasData(
                new Note
                {
                    Id = 1,
                    Title = "Rainbow Connection",
                    Description = "Short LGBTQIA+ meet-cute ideas",
                    NoteText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. At consectetur lorem donec massa.",
                    ProjectId = 1,
                });

            modelBuilder.Entity<LogEntry>().HasData(
                new LogEntry
                {
                    Id = 1,
                    WordCount = 100,
                    NumberOfMinutes = 10,
                    ProjectId = 1,
                });

            base.OnModelCreating(modelBuilder);
        }
    }

}
