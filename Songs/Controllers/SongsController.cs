using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Songs.Data;
using Songs.DTOs;
using Songs.Models;

namespace Songs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs
                    .Include(a => a.Artist)
                    .Include(c => c.Category)
                    .ToListAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _context.Songs
                            .Include(a => a.Artist)   
                            .Include(c => c.Category) 
                            .FirstOrDefaultAsync(s => s.Id == id);


            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, SongDTO song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Get existing song
            var songToEdit = await _context.Songs
                .Include(a => a.Artist)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            if(songToEdit is null)
            {
                return NotFound("Song was not found");
            }

            List<Artist> artist = await _context.Artists.Where(a => song.ArtistId.Contains(a.Id)).ToListAsync();

            if (artist is null || artist.Count == 0)
            {
                return NotFound("Artist was not found, add the artist first before adding the artist songs");
            }
            List<Category> category = await _context.Categories.Where(a => song.CategoryId.Contains(a.Id)).ToListAsync();

            if (category is null || category.Count == 0)
            {
                return NotFound("Category was not found. Create the category first before assigning it to songs");
            }

            songToEdit.Title = song.Title;
            songToEdit.Length = song.Length;
            songToEdit.Artist = artist;
            songToEdit.Category = category;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(SongDTO songDto)
        {

            List<Artist> artist = await _context.Artists.Where(a => songDto.ArtistId.Contains(a.Id)).ToListAsync();

            if(artist is null || artist.Count == 0)
            {
                return NotFound("Artist was not found, add the artist first before adding the artist songs");
            }
            List<Category> category = await _context.Categories.Where(a => songDto.CategoryId.Contains(a.Id)).ToListAsync();

            if(category is null || category.Count == 0)
            {
                return NotFound("Category was not found. Create the category first before assigning it to songs");
            }

            var NewSong = new Song
            {
                Title = songDto.Title,
                Length = songDto.Length,
                Artist = artist,
                Category = category
            };

            if(_context.Songs.Any(s => s.Title == songDto.Title))
            {
                return BadRequest("You can't save the same song more than once. There's already a saved song with this title.");
            }
            _context.Songs.Add(NewSong);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = NewSong.Id }, NewSong);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
