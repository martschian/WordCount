using Microsoft.EntityFrameworkCore;
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
        public async Task<LogEntry> GetLogEntryAsync(int projectId, int logEntryId)
        {
            return await _context.LogEntry.FirstOrDefaultAsync( l => l.Id == logEntryId && l.ProjectId == projectId);
        }

        public async Task<Note> GetNoteAsync(int projectId, int noteId)
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
            if (projectId == null)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            return await _context.Project.AnyAsync(p => p.Id == projectId);
        }

        
    }
}
