using Microsoft.EntityFrameworkCore;
using WordGoal.Domain;

namespace WordGoal.Data
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

        
    }

}
