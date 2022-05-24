using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketingCoinBase.Models;
using MarketingCoinBase.Services;
using MarketingCoinBase.IServices;

namespace MarketingCoinBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnerServices _context;

        public PartnersController(IPartnerServices context)
        {
            _context = context;
        }

        // GET: api/Partners
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Partners>>> GetPartners()
        //{
        //    return await _context.AddPartner();
        //}

        // GET: api/Partners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partners>> GetPartners(long id)
        {
            var partners = await _context.GetPartnerByID(id);

            if (partners == null)
            {
                return NotFound();
            }

            return partners;
        }

        //// PUT: api/Partners/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPartners(long id, Partners partners)
        //{
        //    if (id != partners.partnerID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(partners).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PartnersExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Partners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Partners>> PostPartners(Partners partners)
        {
            await _context.AddPartner(partners);
            return CreatedAtAction("GetPartners", new { id = partners.partnerID }, partners);
        }

        //// DELETE: api/Partners/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePartners(long id)
        //{
        //    var partners = await _context.Partners.FindAsync(id);
        //    if (partners == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Partners.Remove(partners);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool PartnersExists(long id)
        //{
        //    return _context.Partners.Any(e => e.partnerID == id);
        //}
    }
}
