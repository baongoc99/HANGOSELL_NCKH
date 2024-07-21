
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

                // Lấy danh sách người dùng và hiển thị
                List<User> users = userrolesService.GetAllUsers();
                return View(users);
            }
            else
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang chủ
                return RedirectToAction("Index", "Home");
            }
        }

        // Hiển thị giao diện lưới (Grid) danh sách người dùng
        public IActionResult GridUserRoles()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                ViewData["Name"] = HttpContext.Session.GetString("Name");
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
            return RedirectToAction("Index", "Home");
        }


        // Hiển thị trang thêm người dùng mới
        public IActionResult CreateUserRoles()
        {
            return View();
        }

        // Thêm người dùng mới
        [HttpPost]
        public IActionResult CreateUserRoles(User user)
        {
            user.RecordCreatedOn = DateTime.Now;
            user.JoinDate = DateTime.Now;
            userrolesService.AddUserRoles(user);
            return RedirectToAction("IndexUserRoles");
        }

        // Hiển thị trang cập nhật thông tin người dùng
        [HttpGet]
        public IActionResult EditUserRoles(int id)
        {
            User user = userrolesService.GetUserById(id);
            return View(user);
        }

        // Cập nhật thông tin người dùng
        [HttpPost]
        public IActionResult UpdateUserRoles(User user)
        {
            if (user.Id > 0)
            {
                var userpass = userrolesService.GetUserById(user.Id);
                user.RecordCreatedOn = DateTime.Now;
                user.Password = userpass.Password;

                // Sử dụng phương thức UpdateUserRoles đã sửa đổi
                userrolesService.UpdateUserRoles(user);
                return RedirectToAction("IndexUserRoles");
            }
            else
            {
                return RedirectToAction("UpdateUserRoles", new { id = user.Id });
            }
        }


        // Xóa người dùng
        public IActionResult DeleteUserRoles(int id)
        {
            userrolesService.DeleteUserRoles(id);
            return RedirectToAction("IndexUserRoles");
        }

        //Đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userid, string password)
        {
            User user = userrolesService.CheckUserUsnamePass(userid, password);
            if (user != null)
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("JoinDate", user.JoinDate?.ToString("yyyy-MM-dd") ?? string.Empty); // Định dạng ngày
                HttpContext.Session.SetString("DateOfBirth", user.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty); // Định dạng ngày
                HttpContext.Session.SetString("PhoneNumber", user.PhoneNumber);
                HttpContext.Session.SetString("Position", user.Position);

                // Đăng nhập thành công, chuyển hướng đến trang chính
                return RedirectToAction("Index", "Home");
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
            ViewData["UserId"] = HttpContext.Session.GetString("UserId");
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Email"] = HttpContext.Session.GetString("Email");
            ViewData["DateOfBirth"] = HttpContext.Session.GetString("DateOfBirth");
            ViewData["JoinDate"] = HttpContext.Session.GetString("JoinDate");
            ViewData["PhoneNumber"] = HttpContext.Session.GetString("PhoneNumber");
            return View();

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
