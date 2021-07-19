using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DTO.DTOs.BlogDtos;

namespace MyPersonalBlog.UI.Areas.Member.ViewComponents
{
    public class MostViewedBlogsVC : ViewComponent
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        public MostViewedBlogsVC(IMapper mapper, IBlogService blogService)
        {
            _blogService = blogService;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke()
        {
            var mostViewedBlogs = _mapper.Map<List<LastBlogListDto>>(_blogService.GetMostViewedBlogs());
            return View(mostViewedBlogs);
        }
    }
}
