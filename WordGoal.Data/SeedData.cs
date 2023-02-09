using WordGoal.Domain;

namespace WordGoal.Data
{
    public class SeedData
    {
        public static async Task InitAsync(WordGoalAPIContext _context)
        {
            var users = new List<User>()
            {
                new User
                {
                    Name = "Björn Elkeman",
                    Email = "bjorn@elkeman.se",
                    Projects = new List<Project>()
                    {
                        new Project
                        {
                            Title = "Depression Is My Middle-Name",
                            Description = "Pitch Black",
                            DueDate = DateTimeOffset.Now.AddDays(30),
                            WordGoalDaily = 1000,
                            WordGoalTotal = 25000,
                            LogEntries = new List<LogEntry>()
                            {
                                new LogEntry
                                {
                                    WordCount = 200,
                                    NumberOfMinutes = 25,
                                    Timestamp = new DateTimeOffset(2022, 12, 21, 13, 30, 00, new TimeSpan(1, 0, 0)),
                                },
                                new LogEntry
                                {
                                    WordCount = 100,
                                    NumberOfMinutes = 10,
                                    Timestamp = DateTimeOffset.Now,
                                }
                            },
                            Notes = new List<Note>()
                        },
                        new Project
                        {
                            Title = "The Great American Novel",
                            Description = "What is says on the tin, baybeeee!",
                            DueDate = DateTimeOffset.Now.AddDays(60),
                            WordGoalDaily = 2000,
                            WordGoalTotal = 50000,
                            LogEntries = new List<LogEntry>() 
                            {
                                new LogEntry
                                {
                                    WordCount = 2000,
                                    NumberOfMinutes = 180,
                                    Timestamp = DateTimeOffset.Now,
                                }
                            },
                            Notes = new List<Note>()
                            {
                                new Note
                                {
                                    Title = "Test Note",
                                    Description = "A note to test things",
                                    NoteText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                }
                            }
                        }
                    },
                    Notes = new List<Note>()
                    {
                                                    
                                new Note
                                {
                                    Title = "Secret Note",
                                    Description = "Secrets that should not be known",
                                    NoteText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                },
                                new Note
                                {
                                    Title = "Rainbow Connection",
                                    Description = "Short LGBTQIA+ meet-cute ideas",
                                    NoteText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. At consectetur lorem donec massa.",
                                }

                            

                    }
                },
                new User
                {
                    Name = "Adam Handoq",
                    Email = "adam@handoq.com"
                }
            };

            _context.AddRange(users);
            await _context.SaveChangesAsync();
        }
    }
}
