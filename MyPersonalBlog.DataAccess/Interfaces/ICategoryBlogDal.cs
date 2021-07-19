using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DataAccess.Interfaces
{
    public interface ICategoryBlogDal : IGenericDal<CategoryBlog>
    {
        void AddToCategory(CategoryBlog categoryBlog);
        void RemoveFromCategory(CategoryBlog categoryBlog);
    }
}
