using Microsoft.EntityFrameworkCore;
using WordCount.API.Entities;

namespace WordCount.API.Data
{
    public class WordCountAPIContext : DbContext
    {
        public WordCountAPIContext(DbContextOptions<WordCountAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Note { get; set; } = default!;

        public DbSet<Project> Project { get; set; }

        public DbSet<LogEntry> LogEntry { get; set; }
    }
}
