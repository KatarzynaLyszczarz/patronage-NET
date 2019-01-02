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
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces("text/plain")]
        public async Task<ActionResult> Mocky()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.mocky.io/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("v2/5c127054330000e133998f85");
                    response.EnsureSuccessStatusCode();
                    //Request.ContentType = "text/plain";
                    return Content (await response.Content.ReadAsStringAsync());
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                    
                }

            }
        }
    }
}
