using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.Business.Interfaces
{
    public interface ICategoryService : IGenericService<Category>
    {
        List<Category> GetBlogCategoriesByBlogId(int blogId);
        List<Category> GetCategoriesWithBlogsCounts();
        List<Category> GetPopulerCategories();
        Category GetCategoryWithBlogsByCategoryId(int categoryId);
    }
}
