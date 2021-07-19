using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.Business.Interfaces
{
    public interface ICategoryBlogService:IGenericService<CategoryBlog>
    {
        void AddToCategory(CategoryBlog categoryBlog);
        void RemoveFromCategory(CategoryBlog categoryBlog);
    }
}
