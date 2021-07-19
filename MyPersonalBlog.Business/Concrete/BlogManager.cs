using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IBlogDal _blogDal;
        public BlogManager(IGenericDal<Blog> genericDal, IBlogDal blogDal) : base(genericDal)
        {
            _blogDal = blogDal;
        }


        public List<Blog> GetBlogsWithCategories()
        {
            return _blogDal.GetBlogsWithCategories();
        }

        public List<Blog> GetBlogsWithCategoriesAndComments()
        {
            return _blogDal.GetBlogsWithCategoriesAndComments();
        }

        public Blog GetBlogWithCategoriesAndComments(int blogId)
        {
            return _blogDal.GetBlogWithCategoriesAndComments(blogId);
        }

        public List<Blog> GetLastFiveBlogs()
        {
            return _blogDal.GetLastFiveBlogs();
        }

        public List<Blog> GetBlogsWithCategoriesAndComments(out int toplamSayfa, int aktifSayfa = 1)
        {
            return _blogDal.GetBlogsWithCategoriesAndComments(out toplamSayfa, aktifSayfa);
        }

        public List<Blog> GetSearchedBlogsWithSearchString(string searchString)
        {
            return _blogDal.GetSearchedBlogsWithSearchString(searchString);
        }

        public List<Blog> GetMostViewedBlogs()
        {
            return _blogDal.GetMostViewedBlogs();
        }
    }
}
