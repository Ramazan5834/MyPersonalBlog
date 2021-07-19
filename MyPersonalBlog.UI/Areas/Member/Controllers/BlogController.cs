using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DTO.DTOs.BlogDtos;
using MyPersonalBlog.Entities.Concrete;
using MyPersonalBlog.UI.Areas.Member.Models;
using MyPersonalBlog.UI.Tools.AppClasses;

namespace MyPersonalBlog.UI.Areas.Member.Controllers
{
    [Area("Member")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public BlogController(IBlogService blogService, ICommentService commentService, IMapper mapper)
        {
            _blogService = blogService;
            _commentService = commentService;
            _mapper = mapper;
        }
        [Route("BlogDetail/{blogId}/{title}")]
        public IActionResult BlogDetail(int blogId)
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "blog", ActiveHeader = "Blog Detay" };
            ViewBag.Comments = _commentService.GetAllCommentsWithSubComments(blogId, null);
            var blog = _blogService.GetBlogWithCategoriesAndComments(blogId);
            blog.ViewsNumber++;
            _blogService.Update(blog);
            return View(blog);
        }


        [HttpPost]
        public bool AddToComment(CommentAddModel commentAddModel)
        {
            try
            {
                _commentService.Add(_mapper.Map<Comment>(commentAddModel));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IActionResult SearchInBlogs(string searchString)
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "blog", ActiveHeader = "Bloglar" };
            var blogs = _mapper.Map<List<BlogListDto>>(_blogService.GetSearchedBlogsWithSearchString(searchString));
            ViewBag.SearchingValue = searchString;
            return View(blogs);
        }
    }
}
