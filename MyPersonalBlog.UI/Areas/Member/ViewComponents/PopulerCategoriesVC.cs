using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPersonalBlog.Business.Interfaces;

namespace MyPersonalBlog.UI.Areas.Member.ViewComponents
{
    public class PopulerCategoriesVC : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public PopulerCategoriesVC(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            var populerCategories = _categoryService.GetPopulerCategories();
            return View(populerCategories);
        }
    }
}
