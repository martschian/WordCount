using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using WordGoal.Domain;

namespace WordGoal.Data
{
    public class WordGoalRepository : IWordGoalRepository
    {
        private readonly WordGoalAPIContext _context;

        public WordGoalRepository(WordGoalAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogEntry>> GetLogEntriesAsync(int projectId)
        {
            return await _context.LogEntry.Where(l => l.ProjectId == projectId).ToListAsync();
        }
        public async Task<LogEntry?> GetLogEntryAsync(int projectId, int logEntryId)
        {
            return await _context.LogEntry.FirstOrDefaultAsync(l => l.Id == logEntryId && l.ProjectId == projectId);
        }

        public async Task<Note?> GetNoteAsync(int projectId, int noteId)
        {
            return await _context.Note.FirstOrDefaultAsync(n => n.Id == noteId && n.ProjectId == projectId);
        }

        public IQueryable<Note> GetNotes(int projectId)
        {
            return _context.Note.Where(n => n.ProjectId == projectId);
        }

        public async Task<IEnumerable<Note>> GetNotesAsync(int projectId)
        {
            return await _context.Note.Where(n => n.ProjectId == projectId).ToListAsync();
        }

        public async Task<bool> ProjectExistsAsync(int projectId)
        {
            return await _context.Project.AnyAsync(p => p.Id == projectId);
        }

        public void AddLogEntry(LogEntry logEntry, int projectId)
        {
            if (logEntry is null)
            {
                throw new ArgumentNullException(nameof(logEntry));
            }

            logEntry.ProjectId = projectId;
            logEntry.Timestamp = DateTimeOffset.Now;
            _context.LogEntry.Add(logEntry);
        }

        public void DeleteLogEntry(LogEntry logEntry)
        {
            _context.LogEntry.Remove(logEntry);
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateLogEntry(LogEntry logEntry)
        {
            throw new NotImplementedException();
        }

        public void AddNote(Note note, int projectId)
        {
            if (note is null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            note.ProjectId = projectId;

            _context.Note.Add(note);
        }

        public void DeleteNote(Note note)
        {
            _context.Note.Remove(note);
        }

        public async Task<Project?> GetProjectAsync(int userId, int projectId)
        {
            return await _context.Project
                .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(int userId)
        {
            return await _context.Project
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public void AddProject(Project project, int userId)
        {
            if (project is null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            project.UserId = userId;

            _context.Project.Add(project);
        }

        public void DeleteProject(Project project)
        {
            _context.Project.Remove(project);
        }
    }
}
