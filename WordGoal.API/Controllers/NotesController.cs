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
            //if ((await _context.Project.AnyAsync(p => p.Id == projectId)) is false)

            if(!await _repo.ProjectExistsAsync(projectId))
                return NotFound();
            
            //return Ok(_mapper.ProjectTo<NoteDto>(_context.Note.Where(n => n.ProjectId == projectId)).ToListAsync());
            
            var notes = await _repo.GetNotesAsync(projectId);

            return Ok(_mapper.Map<IEnumerable<NoteDto>>(_repo.GetNotesAsync(projectId)));
            
            //return Ok(_mapper.Map<IEnumerable<NoteDto>>(notes));

            //return Ok(_mapper.Map<IEnumerable<NoteDto>>(await 
            //    _context.Note
            //    .Where(n => n.ProjectId == projectId)
            //    .Include(n => n.Project)
            //    .ToListAsync()));
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult<NoteDto>> GetNoteForProject(int projectId, int noteId)
        {
            //if ((await _context.Project.AnyAsync(p => p.Id == projectId)) is false)
            if (!await _repo.ProjectExistsAsync(projectId))
                return NotFound();

            var note = _repo.GetNoteAsync(projectId, noteId);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NoteDto>(note));
        }

        // PUT: api/Notes/5
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

        // POST: api/Notes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
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
