using Microsoft.AspNetCore.Mvc;

namespace NCKH_HANGOSELL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                ViewData["UserId"] = HttpContext.Session.GetString("UserId");
                ViewData["Name"] = HttpContext.Session.GetString("Name");
                ViewData["Email"] = HttpContext.Session.GetString("Email");
                ViewData["DateOfBirth"] = HttpContext.Session.GetString("DateOfBirth");
                ViewData["JoinDate"] = HttpContext.Session.GetString("JoinDate");
                ViewData["PhoneNumber"] = HttpContext.Session.GetString("PhoneNumber");
                ViewData["Position"] = HttpContext.Session.GetString("Position");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Images()
        {
            return View();
        }
    }
}
