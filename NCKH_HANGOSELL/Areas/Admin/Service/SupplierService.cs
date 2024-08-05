using Microsoft.AspNetCore.Mvc;

namespace NCKH_HANGOSELL.Areas.Admin.Service
{
    public class SupplierService : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
