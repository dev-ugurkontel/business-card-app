using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardAPI.Data;
using CardAPI.Entities;
using CardAPI.Models.DTOs.Responses;
using CardAPI.Models.DTOs.Requests;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CardAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly CardAPIContext _context;

        public CardsController(CardAPIContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cards>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cards>> GetCards(int id)
        {
            var cards = await _context.Cards.FindAsync(id);

            if (cards == null)
            {
                return NotFound();
            }

            return cards;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCards(int id, CardsRequest cards)
        {
            if (id <= 0)
                return BadRequest("Not a valid card id");

            try
            {
                var entity = _context.Cards.FirstOrDefault(item => item.Id == id);

                if (entity != null)
                {
                    entity.Name = cards.Name;
                    entity.Title = cards.Title;
                    entity.Phone = cards.Phone;
                    entity.Email = cards.Email;
                    entity.Address = cards.Address;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new CardsResponse()
            {
                Success = true
            });
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cards>> PostCards(Cards cards)
        {
            _context.Cards.Add(cards);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCards", new { id = cards.Id }, cards);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCards(int id)
        {
            var cards = await _context.Cards.FindAsync(id);
            if (cards == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(cards);
            await _context.SaveChangesAsync();

            return Ok(new CardsResponse()
            {
                Success = true
            });
        }

        private bool CardsExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
