using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.Business.Concrete
{
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(IGenericDal<Category> genericDal, ICategoryDal categoryDal) : base(genericDal)
        {
            _categoryDal = categoryDal;
        }


        public List<Category> GetBlogCategoriesByBlogId(int blogId)
        {
            return _categoryDal.GetBlogCategoriesByBlogId(blogId);
        }

        public List<Category> GetCategoriesWithBlogsCounts()
        {
            return _categoryDal.GetCategoriesWithBlogsCounts();
        }

        public List<Category> GetPopulerCategories()
        {
            return _categoryDal.GetPopulerCategories();
        }

        public Category GetCategoryWithBlogsByCategoryId(int categoryId)
        {
            return _categoryDal.GetCategoryWithBlogsByCategoryId(categoryId);
        }
    }
}
