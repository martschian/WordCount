using WordGoal.Domain;

namespace WordGoal.Data
{
    public interface IWordGoalRepository
    {
        Task<Note> GetNoteAsync(int projectId, int noteId);
        Task<IEnumerable<Note>> GetNotesAsync(int projectId);
        Task<LogEntry> GetLogEntryAsync(int projectId, int noteId);
        Task<IEnumerable<LogEntry>> GetLogEntriesAsync(int projectId);
        IQueryable<Note> GetNotes(int projectId);
        Task<bool> ProjectExistsAsync(int projectId);
    }
}