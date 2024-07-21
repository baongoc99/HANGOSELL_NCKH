using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCKH_HANGOSELL.Data;
using NCKH_HANGOSELL.Models;

namespace NCKH_HANGOSELL.Areas.Admin.Service
{
    public class UserRolesService
    {
        private readonly ApplicationDbContext _context;
        public UserRolesService(ApplicationDbContext context)
        {
            _context = context;
        }


        // Lấy danh sách tất cả người dùng
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        // Thêm người dùng mới
        public void AddUserRoles(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Lấy thông tin người dùng theo ID
        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        // Cập nhật thông tin người dùng
        public void UpdateUserRoles(User user)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
            }
            else
            {
                _context.Users.Update(user);
            }
            _context.SaveChanges();
        }


        // Xóa người dùng
        public void DeleteUserRoles(int id)
        {
            var userToDelete = _context.Users.Find(id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }

        // Đăng nhập người dùng
        public User CheckUserUsnamePass(string userid, string password)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserId == userid && u.Password == password);
            return user;
        }
    }
}
