using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CMLSmartHomeController.Model;
using CMLSmartHomeController.Models;
using System;

namespace CMLSmartHomeController.Controllers
{
    /// <summary>
    /// SensorRecordsController
    /// </summary>
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
            return _context.SensorRecords;
        }

        // GET: api/SensorRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorRecord([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensorRecord = await _context.SensorRecords.FindAsync(id);

            if (sensorRecord == null)
            {
                return NotFound();
            }

            return Ok(sensorRecord);
        }

        //[HttpGet("{collectorId}/{sensorId}/{recordDatetimeFrom}/{recordDatetimeTo}")]
        [Route("search")]
        [HttpGet]
        public IEnumerable<SensorRecord> GetSensorRecord([FromQuery] long collectorId, [FromQuery] long sensorId, [FromQuery] DateTime recordDatetimeFrom, [FromQuery] DateTime recordDatetimeTo)
        {
            _logger.LogInformation("api/SensorRecords/collectorId={0}&sensorId={1}&recordDatetimeFrom={2}&recordDatetimeFrom={3}", collectorId, sensorId, recordDatetimeFrom, recordDatetimeTo);

             var sensorRecords = _context.SensorRecords.Where(t =>
                                (collectorId != 0 && t.CollectorId == collectorId) &&
                                (sensorId != 0 && t.SensorId == sensorId) &&
                                (recordDatetimeFrom != DateTime.MaxValue && t.DateTime > recordDatetimeFrom)
                                );

            return sensorRecords;
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
            _logger.LogInformation("SensorRecords: SensorId:{0},  Value:{1} - Start ", sensorRecord.SensorId, sensorRecord.Value);

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sensorRecord);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sensorRecord.DateTime = DateTime.Now;
            _context.SensorRecords.Add(sensorRecord);
            await _context.SaveChangesAsync();

            _logger.LogInformation("SensorRecords - End");

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

            var sensorRecord = await _context.SensorRecords.FindAsync(id);
            if (sensorRecord == null)
            {
                return NotFound();
            }

            _context.SensorRecords.Remove(sensorRecord);
            await _context.SaveChangesAsync();

            return Ok(sensorRecord);
        }

        private bool SensorRecordExists(long id)
        {
            return _context.SensorRecords.Any(e => e.Id == id);
        }
    }
}