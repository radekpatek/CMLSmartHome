using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMLSmartHome.Models;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace CMLSmartHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Získání lokální IP adresy 
        /// </summary>
        private IPAddress LocalIPAddress()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        public ControllersController(ApplicationDbContext context)
        {
            _context = context;

            if (_context.Controller.Count() == 0)
            {
                var macAddress = NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .Select(nic => nic.GetPhysicalAddress().ToString())
                    .FirstOrDefault();

                _context.Controller.Add(new SmartHomeController { Name = "Main SmartHome Controller", MACAddress = macAddress, IPAddress = LocalIPAddress().ToString() });
                _context.SaveChanges();
            }

        }

        // GET: api/Controllers
        [HttpGet]
        public IEnumerable<SmartHomeController> GetController()
        {
            return _context.Controller;
        }

        // GET: api/Controllers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetController([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var controller = await _context.Controller.FindAsync(id);

            if (controller == null)
            {
                return NotFound();
            }

            return Ok(controller);
        }

        // PUT: api/Controllers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutController([FromRoute] long id, [FromBody] SmartHomeController controller)
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
        public async Task<IActionResult> PostController([FromBody] SmartHomeController controller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Controller.Add(controller);
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

            var controller = await _context.Controller.FindAsync(id);
            if (controller == null)
            {
                return NotFound();
            }

            _context.Controller.Remove(controller);
            await _context.SaveChangesAsync();

            return Ok(controller);
        }

        private bool ControllerExists(long id)
        {
            return _context.Controller.Any(e => e.Id == id);
        }
    }
}