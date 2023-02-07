using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordGoal.API.Models;
using WordGoal.Data;
using WordGoal.Domain;

namespace WordGoal.API.Controllers
{
    [Route("api/projects/{projectId}/logentries")]
    [ApiController]
    public class LogEntriesController : ControllerBase
    {
        private readonly WordGoalAPIContext _context;
        private readonly IMapper _mapper;
        private readonly IWordGoalRepository _repo;

        public LogEntriesController(WordGoalAPIContext context, IMapper mapper, IWordGoalRepository repo)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogEntryDto>>> GetLogEntriesForProject(int projectId)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<LogEntryDto>>(await
                _repo.GetLogEntriesAsync(projectId)));
        }

        [HttpGet("{logEntryId}")]
        public async Task<ActionResult<LogEntryDto>> GetLogEntryForProject(int logEntryId, int projectId)
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

        // PUT: api/LogEntries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{logEntryId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutLogEntry(int projectId, int logEntryId, LogEntryForCreationDto logEntry)
        {

            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();
            
            var logEntryToModify = await _repo.GetLogEntryAsync(projectId, logEntryId);
            if (logEntryToModify == null) 
                return NotFound();
            
            _mapper.Map(logEntry, logEntryToModify);
            await _repo.SaveAsync();

            //_context.Entry(logEntry).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!LogEntryExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            return Ok(_mapper.Map<LogEntryDto>(logEntryToModify));
            //return NoContent();
        }

        // POST: api/LogEntries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<LogEntry>> PostLogEntry(int projectId, LogEntryForCreationDto logEntry)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            var logEntryEntity = _mapper.Map<LogEntry>(logEntry);

            _repo.AddLogEntry(logEntryEntity, projectId);
            await _repo.SaveAsync();            
            
            var logEntryToReturn = _mapper.Map<LogEntryDto>(logEntryEntity);

            return CreatedAtAction("GetLogEntryForProject", new { projectId, logEntryId = logEntryToReturn.Id }, logEntryToReturn);
            //return CreatedAtRoute("GetCourseForAuthor", new { authorId, courseId = logEntryToReturn.Id },
            //courseToReturn);
        }

        // DELETE: api/LogEntries/5
        [HttpDelete("{logEntryId}")]
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

        private bool LogEntryExists(int id)
        {
            return _context.LogEntry.Any(e => e.Id == id);
        }
    }
}
