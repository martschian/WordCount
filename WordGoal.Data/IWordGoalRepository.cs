using WordGoal.Domain;

namespace WordGoal.Data
{
    public interface IWordGoalRepository
    {
        Task<Note?> GetNoteAsync(int projectId, int noteId);
        Task<IEnumerable<Note>> GetNotesAsync(int projectId);
        Task<LogEntry?> GetLogEntryAsync(int projectId, int logEntryId);
        Task<IEnumerable<LogEntry>> GetLogEntriesAsync(int projectId);
        IQueryable<Note> GetNotes(int projectId);
        Task<bool> ProjectExistsAsync(int projectId);
        void AddLogEntry(LogEntry logEntry, int projectId);
        void UpdateLogEntry(LogEntry logEntry);
        void DeleteLogEntry(LogEntry logEntry);
        Task<bool> SaveAsync();
        void AddNote(Note note, int projectId);
        void DeleteNote(Note note);
        Task<Project?> GetProjectAsync(int userId, int projectId);
        Task<IEnumerable<Project>> GetProjectsAsync(int userId);
        void AddProject(Project projectEntity, int userId);
        void DeleteProject(Project project);
    }

}