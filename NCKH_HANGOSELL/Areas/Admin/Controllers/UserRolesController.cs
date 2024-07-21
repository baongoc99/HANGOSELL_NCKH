
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
                // Nếu đã đăng nhập, lấy danh sách người dùng và hiển thị
                List<User> users = userrolesService.GetAllUsers();
                return View(users);
            }
            else
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang chủ
                return RedirectToAction("Index", "Home");
            }
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "UserRoles");
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
            user.JoinDate = DateTime.UnixEpoch;
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
                user.Avatar = "1";
                user.JoinDate = DateTime.Now;
                user.RememberToken = "oke";

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
                ///luuw thoong tin login vaf luu vaof session
                HttpContext.Session.SetString("UserId", user.UserId);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("Email", user.Email);
                // Đăng nhập thành công, chuyển hướng đến trang chính
                return RedirectToAction("", "Home");
            }

            return View();
        }
    }
    }
