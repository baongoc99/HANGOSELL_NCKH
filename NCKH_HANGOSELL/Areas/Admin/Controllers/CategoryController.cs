using Microsoft.AspNetCore.Mvc;
using NCKH_HANGOSELL.Areas.Admin.Service;
using NCKH_HANGOSELL.Models;

namespace NCKH_HANGOSELL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        // Hiển thị danh sách loai
        public IActionResult IndexCategory()
        {
            // Kiểm tra nếu người dùng đã đăng nhập
            if (HttpContext.Session.GetString("UserId") != null)
            {
                // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
                ViewData["Name"] = HttpContext.Session.GetString("Name");
                ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
                ViewData["Position"] = HttpContext.Session.GetString("Position");


                // Nếu đã đăng nhập, lấy danh sách người dùng và hiển thị
                List<Category> category = categoryService.GetAllCategory();
                return View(category);
            }
            else
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang login
                return RedirectToAction("Login", "Home");
            }
        }


        // Hiển thị trang thêm loai mới
        public IActionResult CreateCategory()
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");
            return View();
        }

        // Thêm loai mới
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            categoryService.AddCategory(category);
            return Redirect($"/Admin/Category/IndexCategory");
        }

        // Hiển thị trang cập nhật thông tin loai
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");

            Category category = categoryService.GetCategoryById(id);
            return View(category);
        }

        // Cập nhật thông tin loai
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            // Sử dụng phương thức UpdateCategory đã sửa đổi
            categoryService.UpdateCategory(category);
            return Redirect($"/Admin/Category/IndexCategory");

        }


        // Xóa loai
        public IActionResult DeleteCategory(int id)
        {
            categoryService.DeleteCategory(id);
            return Redirect($"/Admin/Category/IndexCategory");
        }


    }
}
