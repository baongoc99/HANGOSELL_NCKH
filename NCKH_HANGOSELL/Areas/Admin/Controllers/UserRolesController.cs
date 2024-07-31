
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NCKH_HANGOSELL.Areas.Admin.Service;
using NCKH_HANGOSELL.Data;
using NCKH_HANGOSELL.Models;

namespace NCKH_HANGOSELL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRolesController : Controller
    {
        private readonly UserRolesService userrolesService;

        public UserRolesController(UserRolesService userrolesService)
        {
            this.userrolesService = userrolesService;
        }


        // Hiển thị danh sách người dùng
        public IActionResult IndexUserRoles()
        {
            // Kiểm tra nếu người dùng đã đăng nhập
            if (HttpContext.Session.GetString("UserId") != null)
            {
                // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
                ViewData["Name"] = HttpContext.Session.GetString("Name");
                ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
                ViewData["Position"] = HttpContext.Session.GetString("Position");


                // Nếu đã đăng nhập, lấy danh sách người dùng và hiển thị
                List<User> users = userrolesService.GetAllUsers();
                return View(users);
            }
            else
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang login
                return RedirectToAction("Login", "Home");
            }
        }

        // Hiển thị giao diện lưới (Grid) danh sách người dùng
        public IActionResult GridUserRoles()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
                ViewData["Name"] = HttpContext.Session.GetString("Name");
                ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
                ViewData["Position"] = HttpContext.Session.GetString("Position");
                List<User> users = userrolesService.GetAllUsers();
                return View(users);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }


        // Hiển thị trang thêm người dùng mới
        public IActionResult CreateUserRoles()
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");
            return View();
        }

        // Thêm người dùng mới
        [HttpPost]
        public IActionResult CreateUserRoles(User user, IFormFile image)
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
                user.Avatar = relativePath;
            }
            else
            {
                user.Avatar = "images/default.jpg" ; 
            }
            user.RecordCreatedOn = DateTime.Now;
            userrolesService.AddUserRoles(user);
            return Redirect($"/Admin/UserRoles/IndexUserRoles");
        }

        // Hiển thị trang cập nhật thông tin người dùng
        [HttpGet]
        public IActionResult EditUserRoles(int id)
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");

            User user = userrolesService.GetUserById(id);
            return View(user);
        }

        // Cập nhật thông tin người dùng
        [HttpPost]
        public IActionResult UpdateUserRoles(User user, IFormFile image)
        {
            if (user.Id > 0)
            {
                var userpass = userrolesService.GetUserById(user.Id);

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
                    user.Avatar = relativePath;
                }
                else
                {
                    user.Avatar = userpass.Avatar;
                }
                user.RecordCreatedOn = DateTime.Now;
                user.Password = userpass.Password;

                // Sử dụng phương thức UpdateUserRoles đã sửa đổi
                userrolesService.UpdateUserRoles(user);
                return Redirect($"/Admin/UserRoles/IndexUserRoles");
            }
            else
            {
                return Redirect($"/Admin/UserRoles/UpdateUserRoles?id={user.Id}");

            }
        }


        // Xóa người dùng
        public IActionResult DeleteUserRoles(int id)
        {
            userrolesService.DeleteUserRoles(id);
            return Redirect($"/Admin/UserRoles/IndexUserRoles");
        }

        //Đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logins(string userid, string password)
        {
            User user = userrolesService.CheckUserUsnamePass(userid, password);
            if (user != null)
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetInt32("Id", user.Id);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("JoinDate", user.JoinDate?.ToString("yyyy-MM-dd") ?? string.Empty); // Định dạng ngày
                HttpContext.Session.SetString("DateOfBirth", user.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty); // Định dạng ngày
                HttpContext.Session.SetString("PhoneNumber", user.PhoneNumber);
                HttpContext.Session.SetString("Position", user.Position);
                HttpContext.Session.SetString("Avatar", user.Avatar);

                // Đăng nhập thành công, chuyển hướng đến trang chính
                return Redirect($"/Admin/Home/");
            }

            // Nếu đăng nhập thất bại, hiển thị lại trang đăng nhập với thông báo lỗi
            ModelState.AddModelError("", "Tên người dùng hoặc mật khẩu không đúng.");
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            // Xử lý thay đổi mật khẩu
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ProfilePage()
        {
            int id = HttpContext.Session.GetInt32("Id").Value;

            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");

            User user = userrolesService.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditProfile(User model)
        {
            // Xử lý chỉnh sửa hồ sơ
            return RedirectToAction("Index");
        }

        // Hiển thị chi tiết thông tin người dùng
        public IActionResult DetailUserRoles(int id)
        {
            // Lấy thông tin người dùng từ dịch vụ theo id
            User user = userrolesService.GetUserById(id);

            // Kiểm tra nếu người dùng tồn tại
            if (user == null)
            {
                // Nếu người dùng không tồn tại, chuyển hướng đến trang danh sách người dùng với thông báo lỗi
                TempData["ErrorMessage"] = "Người dùng không tồn tại.";
                return RedirectToAction("IndexUserRoles");
            }

            // Trả về view chi tiết người dùng
            return View(user);
        }

      
    }
}
