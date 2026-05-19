using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NyiratiBalint_WebAPI.Data;
using NyiratiBalint_WebAPI.Dtos.Partner;
using NyiratiBalint_WebAPI.Models;

namespace NyiratiBalint_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerekController : ControllerBase
    {
        private readonly SzerzodesDbContext _context;

        public PartnerekController(SzerzodesDbContext context)
        {
            _context = context;
        }

        // GET: api/Partnerek
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartnerReadDto>>> GetPartnerek()
        {
            return await _context.Partnerek.Select(p => new PartnerReadDto
            {
                Id = p.Id,
                PartnerNev = p.PartnerNev,
                Email = p.Email,
            }).ToListAsync();
        }

        // GET: api/Partnerek/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartnerReadDto>> GetPartner(int id)
        {
            var partner = await _context.Partnerek.FindAsync(id);

            if (partner == null)
            {
                return NotFound();
            }

            return new PartnerReadDto
            {
                Id = partner.Id,
                PartnerNev = partner.PartnerNev,
                Email = partner.Email,
            };
        }

        // PUT: api/Partnerek/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartner(int id, PartnerUpdateDto partnerDto)
        {
            if (id != partnerDto.Id)
            {
                return BadRequest();
            }

            var partner = _context.Partnerek.Find(id);

            if (partner == null)
            {
                return NotFound();
            }

            partner.PartnerNev = partnerDto.PartnerNev;
            partner.Email = partnerDto.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (!PartnerExists(id))
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

        // POST: api/Partnerek
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PartnerReadDto>> PostPartner(PartnerCreateDto partnerDto)
        {
            var partner = new Partner
            {
                PartnerNev = partnerDto.PartnerNev,
                Email = partnerDto.Email
            };
            _context.Partnerek.Add(partner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartner", new { id = partner.Id }, partner);
        }

        // DELETE: api/Partnerek/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartner(int id)
        {
            var partner = await _context.Partnerek.FindAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            _context.Partnerek.Remove(partner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartnerExists(int id)
        {
            return _context.Partnerek.Any(e => e.Id == id);
        }
    }
}
