using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DataAccess.Interfaces
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        List<Blog> GetBlogsWithCategories();
        List<Blog> GetBlogsWithCategoriesAndComments();
        Blog GetBlogWithCategoriesAndComments(int blogId);
        List<Blog> GetLastFiveBlogs();
        List<Blog> GetBlogsWithCategoriesAndComments(out int toplamSayfa, int aktifSayfa = 1);
        List<Blog> GetSearchedBlogsWithSearchString(string searchString);
        List<Blog> GetMostViewedBlogs();
    }
}
