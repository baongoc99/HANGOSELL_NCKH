using Microsoft.AspNetCore.Mvc;

namespace NCKH_HANGOSELL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
