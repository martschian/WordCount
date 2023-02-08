using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using WordGoal.API.Models;
using WordGoal.Data;
using WordGoal.Domain;

namespace WordGoal.API.Controllers
{
    [Route("api/projects/{projectId}/notes")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IWordGoalRepository _repo;
        // private readonly WordGoalAPIContext _context;
        private readonly IMapper _mapper;

        public NotesController(IWordGoalRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId">The id of the project</param>
        /// <returns>An IEnumerable of NoteDtos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotesForProject(int projectId)
        {
            if(!await _repo.ProjectExistsAsync(projectId))
                return NotFound();
            
            return Ok(_mapper.Map<IEnumerable<NoteDto>>(await _repo.GetNotesAsync(projectId)));
            
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult<NoteDto>> GetNoteForProject(int projectId, int noteId)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            var note = _repo.GetNoteAsync(projectId, noteId);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NoteDto>(note));
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{noteId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutNote(int projectId, int noteId, NoteForCreationDto note)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            var noteToModify = await _repo.GetNoteAsync(projectId, noteId);

            if (noteToModify == null)
                return NotFound();

            _mapper.Map(note, noteToModify);
            await _repo.SaveAsync();

            return Ok(_mapper.Map<NoteDto>(noteToModify));
        }

        /// <summary>
        /// Creates a Note.
        /// </summary>
        /// <param name="note"></param>
        /// /// <param name="projectId"></param>
        /// <returns>A newly created Note</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /projects/{projectId}/notes
        ///     {
        ///        "title": "Note #1",
        ///        "description": "Description of Note #1",
        ///        "noteText": "The full text of Note #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NoteDto>> PostNote(int projectId, NoteForCreationDto note)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            var noteEntity = _mapper.Map<Note>(note);
            _repo.AddNote(noteEntity, projectId);
            await _repo.SaveAsync();

            var noteToReturn = _mapper.Map<NoteDto>(noteEntity);
            
            return CreatedAtAction("GetNoteForProject", new { projectId, noteId = noteToReturn.Id }, noteToReturn);
        }

        /// <summary>
        /// Deletes a note
        /// </summary>
        /// <param name="projectId">The id of the project the note is attached to</param>
        /// <param name="noteId">The id of the note to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{noteId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteNote(int projectId, int noteId)
        {
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            var note = await _repo.GetNoteAsync(projectId, noteId);

            if (note == null)
            {
                return NotFound();
            }

            _repo.DeleteNote(note);
            await _repo.SaveAsync();

            return NoContent();
        }
    }
}
