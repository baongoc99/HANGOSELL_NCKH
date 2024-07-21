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
        public IActionResult Index()
        {
            return View();
        }

     /*   public IActionResult Logins(string userid, string password)
        {
            User user = CheckUserUsnamePass(userid, password);
            if (user != null)
            {
                return 
            }
        }

        private User CheckUserUsnamePass(string userid, string password)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserId == userid && u.Password == password);
            return user;
        }*/
    }
}
