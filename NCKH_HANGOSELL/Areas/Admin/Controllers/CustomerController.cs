using Microsoft.AspNetCore.Mvc;
using NCKH_HANGOSELL.Areas.Admin.Service;
using NCKH_HANGOSELL.Models;

namespace NCKH_HANGOSELL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly CustomerService customerService;

        public CustomerController(CustomerService customerService)
        {
            this.customerService = customerService;
        }

        // Hiển thị danh sách khach hang
        public IActionResult IndexCustomer()
        {
            // Kiểm tra nếu người dùng đã đăng nhập
            if (HttpContext.Session.GetString("UserId") != null)
            {
                // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
                ViewData["Name"] = HttpContext.Session.GetString("Name");
                ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
                ViewData["Position"] = HttpContext.Session.GetString("Position");

                // Nếu đã đăng nhập, lấy danh sách người dùng và hiển thị
                List<Customer> customers = customerService.GetAllCustomers();
                return View(customers);
            }
            else
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang login
                return Redirect($"/Admin/Home");
            }

        }

        // Hiển thị trang thêm khach hang mới
        public IActionResult CreateCustomer()
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");
            return View();
        }

        // Thêm người dùng mới
        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            customer.CustomerType = "Khách sỉ";
            customer.RecordCreatedOn = DateTime.Now;
            customerService.AddCustomer(customer);
            return Redirect($"/Admin/Customer/IndexCustomer");
        }

        // Hiển thị trang cập nhật thông tin khach hang
        [HttpGet]
        public IActionResult EditCustomer(int id)
        {
            // Nếu đã đăng nhập, lưu thông tin người dùng vào ViewData
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Avatar"] = HttpContext.Session.GetString("Avatar");
            ViewData["Position"] = HttpContext.Session.GetString("Position");

            Customer customer = customerService.GetCustomerById(id);
            return View(customer);
        }

        // Cập nhật thông tin người dùng
        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            customer.CustomerType = "Khách sỉ";
            customer.RecordCreatedOn = DateTime.Now;

            // Sử dụng phương thức UpdateCustomer đã sửa đổi
            customerService.UpdateCustomer(customer);
            return Redirect($"/Admin/Customer/IndexCustomer");

        }

        // Xóa khach hang
        public IActionResult DeleteCustomer(int id)
        {
            customerService.DeleteCustomer(id);
            return Redirect($"/Admin/Customer/IndexCustomer");
        }

    }
}
