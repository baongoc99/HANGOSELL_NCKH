using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCKH_HANGOSELL.Data;
using NCKH_HANGOSELL.Models;
using System.Diagnostics;

namespace NCKH_HANGOSELL.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
