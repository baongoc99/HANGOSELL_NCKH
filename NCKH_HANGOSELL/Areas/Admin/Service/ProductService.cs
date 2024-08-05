using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NCKH_HANGOSELL.Data;
using NCKH_HANGOSELL.Models;

namespace NCKH_HANGOSELL.Areas.Admin.Service
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }


        // Lấy danh sách tất cả san pham 
        public List<Product> GetAllProduct()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }
       

        // Thêm Product mới
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Lấy thông tin Product theo ID
        public Product GetProductById(int productId)
        {
            return _context.Products
                           .Include(p => p.Category)
                           .FirstOrDefault(p => p.Id == productId);
        }


        // Cập nhật thông tin Product
        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.SingleOrDefault(u => u.Id == product.Id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(product);
            }
            else
            {
                _context.Products.Update(product);
            }
            _context.SaveChanges();
        }


        // Xóa Product
        public void DeleteProduct(int id)
        {
            var productToDelete = _context.Products.Find(id);
            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
            }
        }
    }
}
