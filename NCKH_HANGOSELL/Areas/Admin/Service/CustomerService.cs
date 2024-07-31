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
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }


        // Lấy danh sách tất cả khach hang
        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        // Thêm khach hang mới
        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        // Lấy thông tin khach hang theo ID
        public Customer GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }

        // Cập nhật thông tin khach hang
        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = _context.Customers.SingleOrDefault(u => u.Id == customer.Id);
            if (existingCustomer != null)
            {
                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            }
            else
            {
                _context.Customers.Update(customer);
            }
            _context.SaveChanges();
        }


        // Xóa khach hang
        public void DeleteCustomer(int id)
        {
            var customerToDelete = _context.Customers.Find(id);
            if (customerToDelete != null)
            {
                _context.Customers.Remove(customerToDelete);
                _context.SaveChanges();
            }
        }

    }
}
