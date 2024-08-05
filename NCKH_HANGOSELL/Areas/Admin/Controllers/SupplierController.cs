using Microsoft.AspNetCore.Mvc;
using NCKH_HANGOSELL.Areas.Admin.Service;
using NCKH_HANGOSELL.Models;
using System.Collections.Generic;

namespace NCKH_HANGOSELL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private readonly SupplierService _supplierService;

        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // Hiển thị danh sách nhà cung cấp
        public IActionResult IndexSupplier()
        {
            // Kiểm tra nếu người dùng đã đăng nhập
            if (HttpContext.Session.GetString("UserId") != null)
            {
                // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
                ViewData["Name"] = HttpContext.Session.GetString("Name");
                ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
                ViewData["Position"] = HttpContext.Session.GetString("Position");

                // Nếu đã đăng nhập, lấy danh sách nhà cung cấp và hiển thị
                List<Supplier> suppliers = _supplierService.GetAllSuppliers();
                return View(suppliers);
            }
            else
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang login
                return RedirectToAction("Login", "Home");
            }
        }

        // Hiển thị trang thêm nhà cung cấp mới
        public IActionResult CreateSupplier()
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");
            return View();
        }

        // Thêm nhà cung cấp mới
        [HttpPost]
        public IActionResult CreateSupplier(Supplier supplier)
        {

            _supplierService.AddSupplier(supplier);
            return Redirect($"/Admin/Supplier/IndexSupplier");

        }

        // Hiển thị trang chỉnh sửa thông tin nhà cung cấp
        [HttpGet]
        public IActionResult EditSupplier(int id)
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");

            Supplier supplier = _supplierService.GetSupplierById(id);
            return View(supplier);
        }

        // Cập nhật thông tin nhà cung cấp
        [HttpPost]
        public IActionResult UpdateSupplier(Supplier supplier)
        {
            _supplierService.UpdateSupplier(supplier);
            return Redirect($"/Admin/Supplier/IndexSupplier");
        }

        // Xóa nhà cung cấp
        public IActionResult DeleteSupplier(int id)
        {
            _supplierService.DeleteSupplier(id);
            return Redirect($"/Admin/Supplier/IndexSupplier");
        }
    }
}
