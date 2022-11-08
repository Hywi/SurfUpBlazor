using Microsoft.AspNetCore.Mvc;

namespace SurfUp.Server.Controllers
{
    public class BoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
