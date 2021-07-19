using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DTO.DTOs.AppUserDtos;
using MyPersonalBlog.Entities.Concrete;
using MyPersonalBlog.UI.Tools.AppClasses;

namespace MyPersonalBlog.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProfileController : Controller
    {
        protected readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private IWebHostEnvironment _webHostEnvironment;
        public ProfileController(ICommentService commentService, UserManager<AppUser> userManager, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _mapper = mapper;
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "myprofile", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            var activeUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            return View(_mapper.Map<AppUserUpdateDto>(activeUser));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AppUserUpdateDto appUserListDto, IFormFile resim)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekKullanici = _userManager.Users.FirstOrDefault(I => I.Id == appUserListDto.Id);
                if (resim != null)
                {
                    string uzanti = Path.GetExtension(resim.FileName);
                    string resimAd = Guid.NewGuid() + uzanti;
                    // string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + resimAd);
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/" + resimAd);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await resim.CopyToAsync(stream);
                    }
                    guncellenecekKullanici.Picture = resimAd;
                }
                guncellenecekKullanici.Name = appUserListDto.Name;
                guncellenecekKullanici.Surname = appUserListDto.SurName;
                guncellenecekKullanici.Email = appUserListDto.Email;
                guncellenecekKullanici.PhoneNumber = appUserListDto.PhoneNumber;
                guncellenecekKullanici.GitHubAddress = appUserListDto.GitHubAddress;
                guncellenecekKullanici.FacebookAddress = appUserListDto.FacebookAddress;
                guncellenecekKullanici.YouTubeAddress = appUserListDto.YouTubeAddress;
                guncellenecekKullanici.LinkedInAddress = appUserListDto.LinkedInAddress;
                guncellenecekKullanici.Autobiography = appUserListDto.Autobiography;
                var result = await _userManager.UpdateAsync(guncellenecekKullanici);
                if (result.Succeeded)
                {
                    TempData["message"] = "Güncelleme işleminiz başarı ile gerçekleşti";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Bir Hata Oluştu");
                return View("Index", appUserListDto);
            }
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "myprofile", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            return View("Index", appUserListDto);
        }
    }
}
