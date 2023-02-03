using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD2.Models.EntityFramework;

namespace TD2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly SeriesDBContext _context;
        public SeriesController(SeriesDBContext context)
        {
            _context = context;
        }
        // GET: api/Series
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Serie))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
        {
            return await _context.Series.ToListAsync();
        }

        // GET: api/Series/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Serie))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Serie>> GetSerie(int id)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            return serie;
        }

        // PUT: api/Series/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Serie))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutSerie(int id, Serie serie)
        {
            if (id != serie.Serieid)
            {
                return BadRequest();
            }
            _context.Entry(serie).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieExists(id))
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
        private bool SerieExists(int id)
        {
            return _context.Series.Any(e => e.Serieid == id);
        }

        // POST: api/Series
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Serie>> PostSerie(Serie serie)
        {
            _context.Series.Add(serie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSerie", new { id = serie.Serieid }, serie);
        }

        // DELETE: api/Series/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSerie(int id)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }

            _context.Series.Remove(serie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
