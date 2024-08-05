using Microsoft.AspNetCore.Mvc;
using NCKH_HANGOSELL.Areas.Admin.Service;
using NCKH_HANGOSELL.Models;

namespace NCKH_HANGOSELL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ProductService productService;
        private readonly CategoryService categoryService;
        public ProductController(ProductService productService, CategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        // Hiển thị danh sách product
        public IActionResult IndexProduct()
        {
            // Kiểm tra nếu người dùng đã đăng nhập
            if (HttpContext.Session.GetString("UserId") != null)
            {
                // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
                ViewData["Name"] = HttpContext.Session.GetString("Name");
                ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
                ViewData["Position"] = HttpContext.Session.GetString("Position");


                // Nếu đã đăng nhập, lấy danh sách Product và hiển thị
                List<Product> product = productService.GetAllProduct();
                return View(product);
            }
            else
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang login
                return RedirectToAction("Login", "Home");
            }
        }

        // Hiển thị trang thêm Product mới
        public IActionResult CreateProduct()
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");
            List<Category> category = categoryService.GetAllCategory();
            return View(category);
        }

        // Thêm Product mới
        [HttpPost]
        public IActionResult CreateProduct(Product product, IFormFile image)
        {
            if (image != null)
            {
                var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Redirect($"/Admin/UserRoles/IndexUserRoles");
                }
                // Ensure unique file name
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
                // Create directory if it doesn't exist
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                try
                {
                    // Save the file
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                }
                catch (Exception ex)
                {
                    // Handle file saving exceptions
                    // Optionally log the error and return an appropriate view
                    return RedirectToAction("Error", "Home");
                }
                var relativePath = Path.Combine("images", uniqueFileName).Replace("\\", "/");
                product.Image = relativePath;
            }
            else
            {
                product.Image = "images/default.jpg";
            }
            productService.AddProduct(product);
            return Redirect($"/Admin/Product/IndexProduct");
        }
       

        // Hiển thị trang cập nhật thông tin Product
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");

            Product product = productService.GetProductById(id);
            List<Category> categories = categoryService.GetAllCategory();

            var viewModel = new EditProductViewModel
            {
                Product = product,
                Categories = categories
            };

            return View(viewModel);
        }

        // Cập nhật thông tin product
        [HttpPost]
        public IActionResult Edit(Product product, IFormFile image)
        {
            if (image != null)
            {
                var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Redirect($"/Admin/UserRoles/IndexUserRoles");
                }
                // Ensure unique file name
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
                // Create directory if it doesn't exist
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                try
                {
                    // Save the file
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                }
                catch (Exception ex)
                {
                    // Handle file saving exceptions
                    // Optionally log the error and return an appropriate view
                    return RedirectToAction("Error", "Home");
                }
                var relativePath = Path.Combine("images", uniqueFileName).Replace("\\", "/");
                product.Image = relativePath;
            }
            else
            {
                product.Image = "images/default.jpg";
            }
            // Sử dụng phương thức UpdateProduct đã sửa đổi
            productService.UpdateProduct(product);
            return Redirect($"/Admin/Product/IndexProduct");

        }


        // Xóa loai
        public IActionResult DeleteProduct(int id)
        {
            productService.DeleteProduct(id);
            return Redirect($"/Admin/Product/IndexProduct");
        }

    }
}
