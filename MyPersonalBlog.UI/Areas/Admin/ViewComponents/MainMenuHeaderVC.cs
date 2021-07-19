using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPersonalBlog.DTO.DTOs.AppUserDtos;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.UI.Areas.Admin.ViewComponents
{
    public class MainMenuHeaderVC:ViewComponent
    {
        protected readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public MainMenuHeaderVC(IMapper mapper, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke()
        {
            var user = _mapper.Map<AppUserListDto>(_userManager.FindByNameAsync("ramazan58").Result); ;
            return View(user);
        }
    }
}
