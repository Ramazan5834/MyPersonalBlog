using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.Business.Concrete
{
    public class CategoryBlogManager : GenericManager<CategoryBlog>, ICategoryBlogService
    {
        private readonly ICategoryBlogDal _categoryBlogDal;
        public CategoryBlogManager(IGenericDal<CategoryBlog> genericDal, ICategoryBlogDal categoryBlogDal) : base(genericDal)
        {
            _categoryBlogDal = categoryBlogDal;
        }

        public void AddToCategory(CategoryBlog categoryBlog)
        {
            _categoryBlogDal.AddToCategory(categoryBlog);
        }

        public void RemoveFromCategory(CategoryBlog categoryBlog)
        {
            _categoryBlogDal.RemoveFromCategory(categoryBlog);
        }
    }
}
