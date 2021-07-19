using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DTO.DTOs.CategoryDtos;
using MyPersonalBlog.UI.Tools.AppClasses;

namespace MyPersonalBlog.UI.Areas.Member.Controllers
{
    [Area("Member")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(IMapper mapper,ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "category", ActiveHeader = "Kategoriler" };
            var categories = _mapper.Map<List<CategoryListDto>>(_categoryService.GetAll());
            return View(categories);
        }

        [Route("CategoryDetail/{categoryId}/{name}")]
        public IActionResult CategoryDetail(int categoryId)
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "category", ActiveHeader = "Kategori Detay" };
            var category = _categoryService.GetCategoryWithBlogsByCategoryId(categoryId);
            return View(category);
        }
    }
}
