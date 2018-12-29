using System;
using Microsoft.AspNetCore.Mvc;


namespace FizzBuzz.Controllers
{
    [Route("api/[controller]")]
    public class FizzBuzzController : Controller
    {
        /// <summary>
        /// Print Fizz when value is divisible by 2 and/or Buzz for 3.
        /// </summary>
        /// <param name="value">It must be beetwen 0-1000</param>
        /// <returns>Fizz, Buzz, FizzBuzz or empty.</returns>
        /// /// <response code="200">Success</response>
        /// <response code="400">Value out of 0-1000</response> 
        [HttpGet("{value}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult <string> FizzBuzz(int value)
        {
            try
            {
                return Methods.FizzBuzz(value);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
