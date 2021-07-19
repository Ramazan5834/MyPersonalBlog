using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.UI.Tools.AppClasses;

namespace MyPersonalBlog.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ICommentService _commentService;
        public HomeController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount()};
            var unconfirmedCommentsCount = _commentService.GetUnconfirmedCommentsCount();
            return View(unconfirmedCommentsCount);
        }
    }
}
