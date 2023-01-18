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
    [Route("api/[controller]")]
    [ApiController]
    public class LogEntriesController : ControllerBase
    {
        private readonly WordGoalAPIContext _context;
        private readonly IMapper _mapper;

        public LogEntriesController(WordGoalAPIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/LogEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogEntry>>> GetLogEntry()
        {
            return Ok(_mapper.Map<IEnumerable<LogEntryDto>>(await _context.LogEntry.ToListAsync()));
        }

        // GET: api/LogEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogEntry>> GetLogEntry(int id)
        {
            var logEntry = await _context.LogEntry.FindAsync(id);

            if (logEntry == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LogEntryDto>(logEntry));
        }

        // PUT: api/LogEntries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogEntry(int id, LogEntry logEntry)
        {
            if (id != logEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(logEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogEntryExists(id))
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

        // POST: api/LogEntries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LogEntry>> PostLogEntry(LogEntry logEntry)
        {
            _context.LogEntry.Add(logEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogEntry", new { id = logEntry.Id }, logEntry);
        }

        // DELETE: api/LogEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogEntry(int id)
        {
            var logEntry = await _context.LogEntry.FindAsync(id);
            if (logEntry == null)
            {
                return NotFound();
            }

            _context.LogEntry.Remove(logEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogEntryExists(int id)
        {
            return _context.LogEntry.Any(e => e.Id == id);
        }
    }
}
