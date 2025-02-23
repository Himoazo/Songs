using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Songs.Data;
using Songs.DTOs;
using Songs.Models;

namespace Songs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, ArtistDTO artistDto)
        {
            if (!ModelState.IsValid || artistDto is null)
            {
                return BadRequest(ModelState);
            }

            var artistToEdit = await _context.Artists.FindAsync(id);

            if (artistToEdit == null)
            {
                return NotFound("Artist was not found");
            }

            artistToEdit.ArtistName = artistDto.ArtistName;
            artistToEdit.Birth = artistDto.Birth;
            artistToEdit.Nationality = artistDto.Nationality;

            _context.Entry(artistToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artists
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(ArtistDTO artistDto)
        {
            if (!ModelState.IsValid || artistDto is null)
            {
                return BadRequest(ModelState);
            }

            Artist artist = new()
            {
                ArtistName = artistDto.ArtistName,
                Birth = artistDto.Birth,
                Nationality = artistDto.Nationality
            };

            if(_context.Artists.Any(a => a.ArtistName == artistDto.ArtistName))
            {
                return BadRequest("Artist already exists in db");
            }

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
