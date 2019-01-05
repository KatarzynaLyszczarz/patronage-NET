using FizzBuzz;
using FizzBuzz.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{

    [TestFixture]
    public class MockyTest
    {
        
        [Test]
        public async Task Mocky_ReturnsExpectedResult()
        {
            Mock<IMockyService> mockMockyService = new Mock<IMockyService>();
            mockMockyService.Setup(service => service.GetContent()).ReturnsAsync("Czary mary");
            MockyController controller = new MockyController(mockMockyService.Object);

            var actionResult = await controller.Mocky();

            Assert.That(actionResult.Result, Is.Null);
            Assert.That(actionResult.Value, Is.EqualTo("Czary mary"));
        }

        [Test]
        public async Task Mocky_BadRequest()
        {
            Mock<IMockyService> mockMockyService = new Mock<IMockyService>();
            mockMockyService.Setup(service => service.GetContent()).Throws<Exception>();
            MockyController controller = new MockyController(mockMockyService.Object);

            var actionResult = await controller.Mocky();
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult.Result);
        }
    }
}
