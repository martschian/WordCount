using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordGoal.API.Data;
using WordGoal.API.Entities;
using WordGoal.API.Models;

namespace WordGoal.API.Controllers
{
    [Route("api/projects/{projectId}/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly WordGoalAPIContext _context;
        private readonly IMapper _mapper;

        public NotesController(WordGoalAPIContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesForProject(int projectId)
        {
            if (await _context.Project.FindAsync(projectId) is null)
                return NotFound();

            return Ok(await _mapper.ProjectTo<NoteDto>(_context.Note.Where(n => n.ProjectId == projectId)).ToListAsync());

            //return Ok(_mapper.Map<IEnumerable<NoteDto>>(await 
            //    _context.Note
            //    .Where(n => n.ProjectId == projectId)
            //    .Include(n => n.Project)
            //    .ToListAsync()));
        }

        // GET: api/Notes/5
        [HttpGet("{noteId}")]
        public async Task<ActionResult<Note>> GetNoteForProject(int noteId)
        {
            var note = await _context.Note.FindAsync(noteId);

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
