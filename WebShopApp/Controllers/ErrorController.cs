using Microsoft.AspNetCore.Mvc;

namespace WebShopApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            return View(code);
        }
    }
}
