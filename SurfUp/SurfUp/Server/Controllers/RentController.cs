using Microsoft.AspNetCore.Mvc;

namespace SurfUp.Server.Controllers
{
    public class RentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
