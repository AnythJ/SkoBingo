using Microsoft.AspNetCore.Mvc;

namespace SkoBingo.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            ViewBag.ErrorStatusCode = statusCode;
            ViewBag.ErrorMessage = "Something went wrong";
            
            return View("NotFound");
        }
    }
}
