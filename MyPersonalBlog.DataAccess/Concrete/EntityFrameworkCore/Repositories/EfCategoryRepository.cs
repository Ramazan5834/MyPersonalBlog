using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryDal
    {
        private MyPersonalBlogContext _context;
        public EfCategoryRepository(MyPersonalBlogContext context) : base(context)
        {
            _context = context;
        }

        public List<Category> GetBlogCategoriesByBlogId(int blogId)
        {
            var blogCategories = _context.CategoryBlogs.Where(I => I.BlogId == blogId).ToList();
            List<Category> categories = new List<Category>();
            foreach (var blogCategory in blogCategories)
            {
                categories.Add(_context.Categories.Find(blogCategory.CategoryId));
            }
            return categories;
        }

        public List<Category> GetCategoriesWithBlogsCounts()
        {
            return _context.Categories.Include(I => I.CategoryBlogs).ToList();
        }

        public List<Category> GetPopulerCategories()
        {
            if (_context.Categories.ToList().Count >= 5)
            {
                return _context.Categories.OrderByDescending(I=>I.CategoryBlogs.Count).Take(5).ToList();
            }
            else
            {
                return _context.Categories.ToList();
            }

        }

        public Category GetCategoryWithBlogsByCategoryId(int categoryId)
        {
            return _context.Categories.Include(I => I.CategoryBlogs)
               .ThenInclude(I => I.Blog).FirstOrDefault(I => I.Id == categoryId);
        }
    }
}
