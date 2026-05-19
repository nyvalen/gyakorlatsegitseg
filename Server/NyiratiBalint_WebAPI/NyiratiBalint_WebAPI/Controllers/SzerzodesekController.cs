using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NyiratiBalint_WebAPI.Data;
using NyiratiBalint_WebAPI.Dtos.Szerzodes;
using NyiratiBalint_WebAPI.Models;

namespace NyiratiBalint_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SzerzodesekController : ControllerBase
    {
        private readonly SzerzodesDbContext _context;

        public SzerzodesekController(SzerzodesDbContext context)
        {
            _context = context;
        }

        // GET: api/Szerzodesek
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SzerzodesReadDto>>> GetSzerzodesek()
        {
            return await _context.Szerzodesek
                .Include(p => p.Partner)
                .Select(sz => new SzerzodesReadDto
                { 
                    Id = sz.Id,
                    SzerzodesSzam = sz.SzerzodesSzam,
                    IgazgatoJovahagyta = sz.IgazgatoJovahagyta,
                    SzerzodesTargya = sz.SzerzodesTargya,
                    PartnerId = sz.PartnerId,
                    PartnerNev = sz.PartnerId != null ? sz.Partner.PartnerNev.ToString() : "Ismeretlen"
                }).ToListAsync();
        }

        // GET: api/Szerzodesek/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SzerzodesReadDto>> GetSzerzodes(int id)
        {
            var szerzodes = await _context.Szerzodesek.Include(p => p.Partner).FirstOrDefaultAsync(sz => sz.Id == id);

            if (szerzodes == null)
            {
                return NotFound();
            }

            return new SzerzodesReadDto
            {
                Id = szerzodes.Id,
                SzerzodesSzam = szerzodes.SzerzodesSzam,
                IgazgatoJovahagyta = szerzodes.IgazgatoJovahagyta,
                SzerzodesTargya = szerzodes.SzerzodesTargya,
                PartnerId = szerzodes.PartnerId,
                PartnerNev = szerzodes.PartnerId != null ? szerzodes.Partner.PartnerNev.ToString() : "Ismeretlen"
            };
        }

        // PUT: api/Szerzodesek/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSzerzodes(int id, SzerzodesUpdateDto szerzodesDto)
        {
            if (id != szerzodesDto.Id)
            {
                return BadRequest();
            }

            var szerzodes = _context.Szerzodesek.Find(id);

            if (szerzodes == null)
            {
                return NotFound(); 
            }

            szerzodes.SzerzodesSzam = szerzodesDto.SzerzodesSzam;
            szerzodes.IgazgatoJovahagyta = szerzodesDto.IgazgatoJovahagyta;
            szerzodes.SzerzodesTargya = szerzodesDto.SzerzodesTargya;
            szerzodes.PartnerId = szerzodesDto.PartnerId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (!SzerzodesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    StatusCode(500, new { message = "Adatbázis hiba történt", Error = ex.Message });
                }
            }

            return NoContent();
        }

        // POST: api/Szerzodesek
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SzerzodesReadDto>> PostSzerzodes(SzerzodesCreateDto szerzodesDto)
        {
            var szerzodes = new Szerzodes
            {
                SzerzodesSzam = szerzodesDto.SzerzodesSzam,
                IgazgatoJovahagyta = szerzodesDto.IgazgatoJovahagyta,
                SzerzodesTargya = szerzodesDto.SzerzodesTargya,
                PartnerId = szerzodesDto.PartnerId,
            };
            _context.Szerzodesek.Add(szerzodes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSzerzodes", new { id = szerzodes.Id }, szerzodes);
        }

        // DELETE: api/Szerzodesek/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSzerzodes(int id)
        {
            var szerzodes = await _context.Szerzodesek.FindAsync(id);
            if (szerzodes == null)
            {
                return NotFound();
            }

            _context.Szerzodesek.Remove(szerzodes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SzerzodesExists(int id)
        {
            return _context.Szerzodesek.Any(e => e.Id == id);
        }
    }
}
