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
    public class EfBlogRepository : EfGenericRepository<Blog>, IBlogDal
    {
        private MyPersonalBlogContext _context;
        public EfBlogRepository(MyPersonalBlogContext context) : base(context)
        {
            _context = context;
        }

        public List<Blog> GetBlogsWithCategories()
        {
            return _context.Blogs.Include(I => I.CategoryBlogs)
                   .ThenInclude(I => I.Category).ToList();
        }


        public List<Blog> GetBlogsWithCategoriesAndComments(out int toplamSayfa, int aktifSayfa = 1)
        {
            var returnValue = _context.Blogs.Include(I => I.Comments)
                .Include(I => I.CategoryBlogs)
                .ThenInclude(I => I.Category).OrderByDescending(I => I.PostedTime);
            toplamSayfa = (int)Math.Ceiling((double)returnValue.Count() / 5);
            return returnValue.Skip((aktifSayfa - 1) * 5).Take(5).ToList();
        }

        public List<Blog> GetSearchedBlogsWithSearchString(string searchString)
        {
            return _context.Blogs.Where(I => I.Title.Contains(searchString) || I.ShortDescription.Contains(searchString) || I.Description.Contains(searchString)).ToList();
        }

        public List<Blog> GetBlogsWithCategoriesAndComments()
        {
            return _context.Blogs.Include(I => I.Comments)
                .Include(I => I.CategoryBlogs)
                .ThenInclude(I => I.Category).ToList();
        }

        public Blog GetBlogWithCategoriesAndComments(int blogId)
        {
            return _context.Blogs.Include(I => I.Comments)
                .Include(I => I.CategoryBlogs)
                .ThenInclude(I => I.Category)
                .FirstOrDefault(I => I.Id == blogId);
        }

        public List<Blog> GetLastFiveBlogs()
        {
            return _context.Blogs.OrderByDescending(I => I.PostedTime).Take(5).ToList();
        }

        public List<Blog> GetMostViewedBlogs()
        {
            return _context.Blogs.OrderByDescending(I => I.ViewsNumber).Take(5).ToList();
        }

    }
}
