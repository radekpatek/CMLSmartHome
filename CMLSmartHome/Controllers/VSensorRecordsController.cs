using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using CMLSmartHomeController.Model;
using CMLSmartHomeController.Models;

namespace CMLSmartHomeController.Controllers
{
    /// <summary>
    /// VSensorRecordsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VSensorRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ILogger<VSensorRecordsController> _logger;

        public VSensorRecordsController(ApplicationDbContext context, ILogger<VSensorRecordsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/SensorRecords
        [HttpGet]
        public IEnumerable<VSensorRecord> GetSensorRecord()
        {
            return _context.VSensorRecords;
        }

        // GET: api/SensorRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorRecord([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensorRecord = await _context.VSensorRecords.FindAsync(id);

            if (sensorRecord == null)
            {
                return NotFound();
            }

            return Ok(sensorRecord);
        }

        //[HttpGet("{collectorId}/{sensorId}/{recordDatetimeFrom}/{recordDatetimeTo}")]
        [Route("search")]
        [HttpGet]
        public IEnumerable<VSensorRecord> GetSensorRecord([FromQuery] long collectorId, [FromQuery] long sensorId, [FromQuery] DateTime recordDatetimeFrom, [FromQuery] DateTime recordDatetimeTo)
        {
            _logger.LogInformation("api/VSensorRecords/collectorId={0}&sensorId={1}&recordDatetimeFrom={2}&recordDatetimeFrom={3}", collectorId, sensorId, recordDatetimeFrom, recordDatetimeTo);

             var sensorRecords = _context.VSensorRecords.Where(t =>
                                (collectorId != 0 && t.CollectorId == collectorId) &&
                                (sensorId != 0 && t.SensorId == sensorId) &&
                                (recordDatetimeFrom != DateTime.MaxValue && t.DateTime > recordDatetimeFrom)
                                );

            return sensorRecords;
        }


        private bool SensorRecordExists(long id)
        {
            return _context.VSensorRecords.Any(e => e.Id == id);
        }
    }
}