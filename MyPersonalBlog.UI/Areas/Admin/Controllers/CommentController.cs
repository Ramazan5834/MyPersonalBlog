using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DTO.DTOs.CommentDtos;
using MyPersonalBlog.UI.Tools.AppClasses;

namespace MyPersonalBlog.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public CommentController(IMapper mapper,ICommentService commentService)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "unconfirmedcomment", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            var unconfirmedComments = _mapper.Map<List<UnconfirmedCommentListDto>>(_commentService.GetUnconfirmedCommentsWithBlog());
            return View(unconfirmedComments);
        }

        public IActionResult ConfirmComment(int commentId)
        {
            var comment = _commentService.GetById(commentId);
            comment.Confirmed = true;
            _commentService.Update(comment);
            return RedirectToAction("Index");
        }

        public bool RejectComment(int id)
        {
            try
            {
                var comment = _commentService.GetById(id);
                _commentService.Remove(comment);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
