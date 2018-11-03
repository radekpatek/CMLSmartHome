using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMLSmartHomeController.Models;
using Microsoft.Extensions.Logging;
using CMLSmartHomeCommon.Model;

namespace CMLSmartHomeController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ILogger<CollectorsController> _logger;

        public CollectorsController(ApplicationDbContext context, ILogger<CollectorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Collectors
        [HttpGet]
        public IEnumerable<Collector> GetCollectors()
        {
            return _context.Collectors;
        }

        [Route("search")]
        [HttpGet]
#pragma warning disable CS1998 // V této asynchronní metodě chybí operátory await a spustí se synchronně.
        public async Task<IActionResult> GetCollectorsByQuery(string macAddress)
#pragma warning restore CS1998 // V této asynchronní metodě chybí operátory await a spustí se synchronně.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collector = _context.Collectors.Include(t => t.Sensors)
                                    .Where(t => t.MACAddress == macAddress).FirstOrDefault();

            if (collector == null)
            {
                return NotFound();
            }

            return Ok(collector);
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