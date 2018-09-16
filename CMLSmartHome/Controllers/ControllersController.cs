using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMLSmartHome.Models;
using System.Net.NetworkInformation;
using System.Net;

namespace CMLSmartHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Get local Host Name
        /// </summary>
        private string LocalHostName()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            return Dns.GetHostName();
        }
        
        public ControllersController(ApplicationDbContext context)
        {
            _context = context;

            if (_context.Controllers.Count() == 0)
            {
                var macAddress = NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .Select(nic => nic.GetPhysicalAddress().ToString())
                    .FirstOrDefault();

                _context.Controllers.Add(new Models.SmartHomeController { Description = "Main SmartHome Controller", MACAddress = macAddress, Name = LocalHostName() });
                _context.SaveChanges();
            }

        }
        
        // GET: api/Controllers
        [HttpGet]
        public IEnumerable<Models.SmartHomeController> GetController()
        {
            return _context.Controllers;
        }

        // GET: api/Controllers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetController([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var controller = await _context.Controllers.FindAsync(id);

            if (controller == null)
            {
                return NotFound();
            }

            return Ok(controller);
        }

        // PUT: api/Controllers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutController([FromRoute] long id, [FromBody] Models.SmartHomeController controller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != controller.Id)
            {
                return BadRequest();
            }

            _context.Entry(controller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControllerExists(id))
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

        // POST: api/Controllers
        [HttpPost]
        public async Task<IActionResult> PostController([FromBody] Models.SmartHomeController controller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Controllers.Add(controller);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetController", new { id = controller.Id }, controller);
        }

        // DELETE: api/Controllers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteController([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var controller = await _context.Controllers.FindAsync(id);
            if (controller == null)
            {
                return NotFound();
            }

            _context.Controllers.Remove(controller);
            await _context.SaveChangesAsync();

            return Ok(controller);
        }

        private bool ControllerExists(long id)
        {
            return _context.Controllers.Any(e => e.Id == id);
        }
    }
}