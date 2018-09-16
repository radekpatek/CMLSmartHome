using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMLSmartHome.Models;
using log4net.Repository.Hierarchy;
using System.Net;
using Microsoft.Extensions.Logging;

namespace CMLSmartHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ILogger<SensorRecordsController> _logger;

        public SensorRecordsController(ApplicationDbContext context, ILogger<SensorRecordsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/SensorRecords
        [HttpGet]
        public IEnumerable<SensorRecord> GetSensorRecord()
        {
            return _context.SensorRecord;
        }

        // GET: api/SensorRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorRecord([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensorRecord = await _context.SensorRecord.FindAsync(id);

            if (sensorRecord == null)
            {
                return NotFound();
            }

            return Ok(sensorRecord);
        }

        // PUT: api/SensorRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorRecord([FromRoute] long id, [FromBody] SensorRecord sensorRecord)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sensorRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(sensorRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorRecordExists(id))
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

        // POST: api/SensorRecords
        [HttpPost]
        public async Task<IActionResult> PostSensorRecord([FromBody] SensorRecord sensorRecord)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sensorRecord);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SensorRecord.Add(sensorRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensorRecord", new { id = sensorRecord.Id }, sensorRecord);
        }

        // DELETE: api/SensorRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorRecord([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensorRecord = await _context.SensorRecord.FindAsync(id);
            if (sensorRecord == null)
            {
                return NotFound();
            }

            _context.SensorRecord.Remove(sensorRecord);
            await _context.SaveChangesAsync();

            return Ok(sensorRecord);
        }

        private bool SensorRecordExists(long id)
        {
            return _context.SensorRecord.Any(e => e.Id == id);
        }
    }
}