using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using CMLSmartHomeCommon.Controllers;
using CMLSmartHomeController.Models;
using CMLSmartHomeCommon.Model;

namespace CMLSmartHomeController.Tests
{
    [TestFixture]
    public class SensorsControllerTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private Mock<DbSet<Sensor>> _mockSet;
        private Mock<ILogger<SensorsController>> _mockLogger;
        private SensorsController _controller;
        private List<Sensor> _sensors;

        [SetUp]
        public void Setup()
        {
            _sensors = new List<Sensor>
            {
                new Sensor { Id = 1, Name = "Sensor1" },
                new Sensor { Id = 2, Name = "Sensor2" }
            };

            _mockSet = new Mock<DbSet<Sensor>>();
            _mockSet.As<IQueryable<Sensor>>().Setup(m => m.Provider).Returns(_sensors.AsQueryable().Provider);
            _mockSet.As<IQueryable<Sensor>>().Setup(m => m.Expression).Returns(_sensors.AsQueryable().Expression);
            _mockSet.As<IQueryable<Sensor>>().Setup(m => m.ElementType).Returns(_sensors.AsQueryable().ElementType);
            _mockSet.As<IQueryable<Sensor>>().Setup(m => m.GetEnumerator()).Returns(_sensors.AsQueryable().GetEnumerator());

            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext.Setup(c => c.Sensors).Returns(_mockSet.Object);

            _mockLogger = new Mock<ILogger<SensorsController>>();

            _controller = new SensorsController(_mockContext.Object, _mockLogger.Object);
        }

        [Test]
        public void GetSensors_ShouldReturnAllSensors()
        {
            var result = _controller.GetSensors();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result, Is.EquivalentTo(_sensors));
        }

        [Test]
        public async Task GetSensor_ShouldReturnSensor_WhenSensorExists()
        {
            var result = await _controller.GetSensor(1) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(_sensors[0]));
        }

        [Test]
        public async Task GetSensor_ShouldReturnNotFound_WhenSensorDoesNotExist()
        {
            var result = await _controller.GetSensor(3);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task PutSensor_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            var sensor = new Sensor { Id = 1, Name = "UpdatedSensor" };
            var result = await _controller.PutSensor(1, sensor);

            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task PutSensor_ShouldReturnBadRequest_WhenIdDoesNotMatch()
        {
            var sensor = new Sensor { Id = 1, Name = "UpdatedSensor" };
            var result = await _controller.PutSensor(2, sensor);

            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task PostSensor_ShouldReturnCreatedAtAction_WhenSensorIsCreated()
        {
            var sensor = new Sensor { Id = 3, Name = "NewSensor" };
            var result = await _controller.PostSensor(sensor) as CreatedAtActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("GetSensor"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(sensor.Id));
            Assert.That(result.Value, Is.EqualTo(sensor));
        }

        [Test]
        public async Task DeleteSensor_ShouldReturnOk_WhenSensorIsDeleted()
        {
            var result = await _controller.DeleteSensor(1) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(_sensors[0]));
        }

        [Test]
        public async Task DeleteSensor_ShouldReturnNotFound_WhenSensorDoesNotExist()
        {
            var result = await _controller.DeleteSensor(3);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
    }
}
