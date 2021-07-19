using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DTO.DTOs.AppUserDtos;
using MyPersonalBlog.Entities.Concrete;
using MyPersonalBlog.UI.Areas.Member.Models;
using MyPersonalBlog.UI.Tools.AppClasses;
using MyPersonalBlog.UI.Tools.Helpers;

namespace MyPersonalBlog.UI.Areas.Member.Controllers
{
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration,IMapper mapper, IBlogService blogService, UserManager<AppUser> userManager)
        {
            _blogService = blogService;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }
        public IActionResult Index(int aktifSayfa = 1)
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "blog", ActiveHeader = "Bloglar" };
            var blogs = _blogService.GetBlogsWithCategoriesAndComments(out int toplamSayfa, aktifSayfa);
            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.AktifSayfa = aktifSayfa;
            return View(blogs);
        }

        public IActionResult AboutMe()
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "aboutme", ActiveHeader = "Benimle İlgili" };
            var admin = _mapper.Map<AppUserListDto>(_userManager.FindByNameAsync("ramazan58").Result);
            return View(admin);
        }

        public IActionResult ContactMe()
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "contactme", ActiveHeader = "Benimle Bağlantı Kur" };
            ViewBag.AdminInfos = _mapper.Map<AppUserListDto>(_userManager.FindByNameAsync("ramazan58").Result);
            return View(new SendMessageModel());
        }

        [HttpPost]
        public IActionResult SendEmail(SendMessageModel sendMessageModel)
        {
            if (ModelState.IsValid)
            {
                EmailHelper emailHelper = new EmailHelper(_configuration);
                bool emailResponse = emailHelper.SendEmail(sendMessageModel);
                if (emailResponse)
                {
                    TempData["message"] = "Mesaj Başarı İle Gönderildi";
                    return RedirectToAction("ContactMe");
                }
                else
                {
                    TempData["errorMessage"] = "Mesaj Gönderilirken Bir Hata Oluştu";
                    return RedirectToAction("ContactMe");
                }
            }
            else
            {
                TempData["errorMessage"] = "Gerekli Alanları Doldurduğunuzdan Emin Olun";
                return RedirectToAction("ContactMe");
            }
        }
    }
}
