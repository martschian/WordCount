using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordGoal.API.Models;
using WordGoal.Data;
using WordGoal.Domain;

namespace WordGoal.API.Controllers
{
    [Route("api/projects/{projectId}/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IWordGoalRepository _repo;
        private readonly WordGoalAPIContext _context;
        private readonly IMapper _mapper;

        public NotesController(IWordGoalRepository repo, IMapper mapper, WordGoalAPIContext context)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a Note.
        /// </summary>
        /// <param name="note"></param>
        /// <returns>A newly created Note</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Projects/{id}/Notes
        ///     {
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            _context.Note.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Note.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}
