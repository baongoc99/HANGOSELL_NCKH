using NCKH_HANGOSELL.Data;
using NCKH_HANGOSELL.Models;

namespace NCKH_HANGOSELL.Areas.Admin.Service
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }


        // Lấy danh sách tất cả loại 
        public List<Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        // Thêm loai mới
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        // Lấy thông tin loai theo ID
        public Category GetCategoryById(int categoryid)
        {
            return _context.Categories.Find(categoryid);
        }

        // Cập nhật thông tin loai
        public void UpdateCategory(Category category)
        {
            var existingCategory = _context.Categories.SingleOrDefault(u => u.CategoryId == category.CategoryId);
            if (existingCategory != null)
            {
                _context.Entry(existingCategory).CurrentValues.SetValues(category);
            }
            else
            {
                _context.Categories.Update(category);
            }
            _context.SaveChanges();
        }


        // Xóa loai
        public void DeleteCategory(int categoryid)
        {
            var categoryToDelete = _context.Categories.Find(categoryid);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                _context.SaveChanges();
            }
        }
    }
}
