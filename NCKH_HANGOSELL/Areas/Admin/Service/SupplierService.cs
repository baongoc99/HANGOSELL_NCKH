using NCKH_HANGOSELL.Data;
using NCKH_HANGOSELL.Models;
using System.Collections.Generic;
using System.Linq;

namespace NCKH_HANGOSELL.Areas.Admin.Service
{
    public class SupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả nhà cung cấp
        public List<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        // Thêm nhà cung cấp mới
        public void AddSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        // Lấy thông tin nhà cung cấp theo ID
        public Supplier GetSupplierById(int supplierId)
        {
            return _context.Suppliers.Find(supplierId);
        }

        // Cập nhật thông tin nhà cung cấp
        public void UpdateSupplier(Supplier supplier)
        {
            var existingSupplier = _context.Suppliers.SingleOrDefault(s => s.SupplyId == supplier.SupplyId);
            if (existingSupplier != null)
            {
                _context.Entry(existingSupplier).CurrentValues.SetValues(supplier);
            }
            else
            {
                _context.Suppliers.Update(supplier);
            }
            _context.SaveChanges();
        }

        // Xóa nhà cung cấp
        public void DeleteSupplier(int supplierId)
        {
            var supplierToDelete = _context.Suppliers.Find(supplierId);
            if (supplierToDelete != null)
            {
                _context.Suppliers.Remove(supplierToDelete);
                _context.SaveChanges();
            }
        }
    }
}
