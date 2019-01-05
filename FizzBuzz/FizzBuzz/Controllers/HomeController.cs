using Microsoft.AspNetCore.Mvc;
using FizzBuzz.Models;

namespace FizzBuzz.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        [Route("error")]
        public IActionResult Error()
        {

            return View(new ErrorViewModel
            {
                message = "Error details."
            });
        }
    }
}