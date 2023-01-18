using WordCount.API.Entities;

namespace WordCount.API.Data
{
    public class SeedData
    {
        public static async Task InitAsync(WordCountAPIContext _context)
        {
            var user = new User
            {
                Name = "Björn Elkeman",
                Email = "bjorn@elkeman.se",
                Projects= new List<Project> { }
            };
            await _context.AddAsync(user);

            var project = new Project
            {
                Title = "Depression Is My Middle-Name",
                Description = "Pitch Black",
                User = user,
                Notes= new List<Note> { },
                LogEntries= new List<LogEntry> { },
            };
            await _context.AddAsync(project);
            
            var logEntry = new LogEntry
            {
                WordCount = 2000,
                NumberOfMinutes = 180,
                Timestamp = DateTimeOffset.Now,
                Project = project,
                
            };
            await _context.AddAsync(logEntry);

            var note = new Note
            {
                Title = "Test Note",
                Description = "A note to test things",
                NoteText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            };
            await _context.AddAsync(note);

            project.Notes.Add(note);
            project.LogEntries.Add(logEntry);
            user.Projects.Add(project);
            
            await _context.SaveChangesAsync();
        }
    }
}
