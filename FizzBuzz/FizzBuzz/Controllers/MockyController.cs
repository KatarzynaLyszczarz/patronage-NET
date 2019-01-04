using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace FizzBuzz.Controllers
{
    [Route("api/[controller]")]
    public class MockyController : Controller
    {
        /// <summary>
        /// Requests http://www.mocky.io/v2/5c127054330000e133998f85 and returns content.
        /// </summary>
        /// <returns>Content</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Failure</response>

        private readonly IMockyService mocky;
        public MockyController(IMockyService mocky)
        { this.mocky = mocky; }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces("text/plain")]
        public async Task<ActionResult<string>> Mocky()
        {
            try
            {
                return await mocky.GetContent();
            }
             catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
