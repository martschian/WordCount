using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordGoal.API.Models;
using WordGoal.Data;
using WordGoal.Domain;

namespace WordGoal.API.Controllers
{
    [Route("api/projects/{projectId}/logentries")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class LogEntriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWordGoalRepository _repo;

        public LogEntriesController(IMapper mapper, IWordGoalRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Gets all log entries for a project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogEntryDto>>> GetLogEntriesForProject(int projectId)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<LogEntryDto>>(await
                _repo.GetLogEntriesAsync(projectId)));
        }
        /// <summary>
        /// Gets the details of a specific log entry
        /// </summary>
        /// <param name="logEntryId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{logEntryId}")]
        public async Task<ActionResult<LogEntryDto>> GetLogEntryForProject(int projectId, int logEntryId)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            var logEntry = await _repo.GetLogEntryAsync(projectId, logEntryId);

            if (logEntry == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LogEntryDto>(logEntry));
        }

        /// <summary>
        /// Updates the log entry with the specified id
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="logEntryId"></param>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        [HttpPut("{logEntryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutLogEntry(int projectId, int logEntryId, LogEntryForCreationDto logEntry)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();
            
            var logEntryToModify = await _repo.GetLogEntryAsync(projectId, logEntryId);
            
            if (logEntryToModify == null) 
                return NotFound();
            
            var timestamp = logEntryToModify.Timestamp;
            _mapper.Map(logEntry, logEntryToModify);
            await _repo.SaveAsync();

            return Ok(_mapper.Map<LogEntryDto>(logEntryToModify));
        }

        /// <summary>
        /// Creates a new log entry tied to the specified projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LogEntryDto>> PostLogEntry(int projectId, LogEntryForCreationDto logEntry)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            var logEntryEntity = _mapper.Map<LogEntry>(logEntry);

            _repo.AddLogEntry(logEntryEntity, projectId);
            await _repo.SaveAsync();            
            
            var logEntryToReturn = _mapper.Map<LogEntryDto>(logEntryEntity);

            return CreatedAtAction("GetLogEntryForProject", new { projectId, logEntryId = logEntryToReturn.Id }, logEntryToReturn);
        }

        /// <summary>
        /// Deletes the specified LogEntry
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="logEntryId"></param>
        /// <returns></returns>
        [HttpDelete("{logEntryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLogEntry(int projectId, int logEntryId)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();
            
            var logEntry = await _repo.GetLogEntryAsync(projectId, logEntryId);

            if (logEntry == null)
            {
                return NotFound();
            }
            
            _repo.DeleteLogEntry(logEntry);
            await _repo.SaveAsync();

            return NoContent();
        }
    }
}
