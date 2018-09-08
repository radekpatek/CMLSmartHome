using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMLSmartHome.Models;

namespace CMLSmartHomeController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CollectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Collectors
        [HttpGet]
        public IEnumerable<Collector> GetCollectors()
        {
            return _context.Collectors;
        }

        // GET: api/Collectors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollector([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collector = await _context.Collectors.FindAsync(id);

            if (collector == null)
            {
                return NotFound();
            }

            return Ok(collector);
        }

        // PUT: api/Collectors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollector([FromRoute] long id, [FromBody] Collector collector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != collector.Id)
            {
                return BadRequest();
            }

            _context.Entry(collector).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectorExists(id))
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

        // POST: api/Collectors
        [HttpPost]
        public async Task<IActionResult> PostCollector([FromBody] Collector collector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Collectors.Add(collector);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollector", new { id = collector.Id }, collector);
        }

        // DELETE: api/Collectors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollector([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collector = await _context.Collectors.FindAsync(id);
            if (collector == null)
            {
                return NotFound();
            }

            _context.Collectors.Remove(collector);
            await _context.SaveChangesAsync();

            return Ok(collector);
        }

        private bool CollectorExists(long id)
        {
            return _context.Collectors.Any(e => e.Id == id);
        }
    }
}