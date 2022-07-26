using Microsoft.AspNetCore.Mvc;

namespace SkoBingo.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public ViewResult Index(int statusCode)
        {
            ViewBag.ErrorStatusCode = statusCode;
            ViewBag.ErrorMessage = "Something went wrong";
            
            return View("NotFound");
        }
    }
}
