using Microsoft.AspNetCore.Mvc;

namespace Utopia.Controllers;

public class ErrorController : Controller
{
    [Route("/Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        ViewBag.statuscode = statusCode;
        return View("error");
    }
}