using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCategoryBlogRepository:EfGenericRepository<CategoryBlog>,ICategoryBlogDal
    {
        private readonly MyPersonalBlogContext _context;
        public EfCategoryBlogRepository(MyPersonalBlogContext context) : base(context)
        {
            _context = context;
        }

        public void AddToCategory(CategoryBlog categoryBlog)
        {
            var control = _context.CategoryBlogs
                .Where(I => I.BlogId == categoryBlog.BlogId)
                .FirstOrDefault(I => I.CategoryId == categoryBlog.CategoryId);
            if (control == null)
            {
                _context.CategoryBlogs.Add(categoryBlog);
                _context.SaveChanges();
            }
        }

        public void RemoveFromCategory(CategoryBlog categoryBlog)
        {
            var controlCategoryBlog = _context.CategoryBlogs
                .Where(I => I.BlogId == categoryBlog.BlogId)
                .FirstOrDefault(I => I.CategoryId == categoryBlog.CategoryId);
            if (controlCategoryBlog != null)
            {
                _context.CategoryBlogs.Remove(controlCategoryBlog);
                _context.SaveChanges();
            }
        }
    }
}
