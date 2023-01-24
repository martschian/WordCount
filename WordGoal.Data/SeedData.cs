using WordGoal.Domain;

namespace WordGoal.Data
{
    public class SeedData
    {
        public static async Task InitAsync(WordGoalAPIContext _context)
        {
            var user = new User
            {
                Name = "Björn Elkeman",
                Email = "bjorn@elkeman.se",
                Projects = new List<Project>()
            };
            _context.Add(user);

            var project = new Project
            {
                Title = "Depression Is My Middle-Name",
                Description = "Pitch Black",
                User = user,
                Notes = new List<Note>(),
                LogEntries = new List<LogEntry>(),
            };
            _context.Add(project);

            var logEntries = new List<LogEntry>() {
                new LogEntry
                {
                    WordCount = 2000,
                    NumberOfMinutes = 180,
                    Timestamp = DateTimeOffset.Now,
                    Project = project,

                },
                new LogEntry{
                    WordCount = 200,
                    NumberOfMinutes = 25,
                    Timestamp = new DateTimeOffset(2022, 12, 21, 13, 30, 00, new TimeSpan(1, 0, 0)),
                    Project= project,
                } 
            };
            _context.AddRange(logEntries);

            var notes = new List<Note>() {
                new Note
            {
                Title = "Test Note",
                Description = "A note to test things",
                NoteText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            },new Note
            {
                Title = "Secret Note",
                Description = "Secrets that should not be known",
                NoteText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            },

            };

            foreach (var note in notes)
            {
                _context.Add(note);
                project.Notes.Add(note);
            }
            
            foreach (var item in logEntries)
            {
                project.LogEntries.Add(item);
            }
            
            user.Projects.Add(project);

            await _context.SaveChangesAsync();
        }
    }
}
