using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DTO.DTOs.CategoryDtos;
using MyPersonalBlog.Entities.Concrete;
using MyPersonalBlog.UI.Tools.AppClasses;

namespace MyPersonalBlog.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        public CategoryController(ICommentService commentService, IMapper mapper, ICategoryService categoryService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "category", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            var categories = _categoryService.GetCategoriesWithBlogsCounts();
            return View(categories);
        }

        public IActionResult CreateOrUpdateCategory(int? categoryId)
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "category", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            if (categoryId != null)
            {
                var updatedCategory = _mapper.Map<CategoryCreateOrUpdateDto>(_categoryService.GetById((int)categoryId));
                return View(updatedCategory);
            }
            else
            {
                return View(new CategoryCreateOrUpdateDto());
            }
        }


        [HttpPost]
        public IActionResult CreateOrUpdateCategory(CategoryCreateOrUpdateDto categoryCreateOrUpdate)
        {
            if (ModelState.IsValid)
            {
                if (categoryCreateOrUpdate.Id == 0)
                {
                    _categoryService.Add(_mapper.Map<Category>(categoryCreateOrUpdate));
                }
                else
                {
                    _categoryService.Update(_mapper.Map<Category>(categoryCreateOrUpdate));
                }
                return RedirectToAction("Index");
            }
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "category", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            return View(categoryCreateOrUpdate);
        }

        [HttpPost]
        public bool RemoveCategory(int id)
        {
            try
            {
                _categoryService.Remove(_categoryService.GetById(id));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
