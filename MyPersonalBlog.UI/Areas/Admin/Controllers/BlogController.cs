using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
using MyPersonalBlog.DTO.DTOs.BlogDtos;
using MyPersonalBlog.DTO.DTOs.CategoryBlogDtos;
using MyPersonalBlog.Entities.Concrete;
using MyPersonalBlog.UI.Areas.Admin.Models;
using MyPersonalBlog.UI.Tools.AppClasses;

namespace MyPersonalBlog.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryBlogService _categoryBlogService;
        private readonly ICommentService _commentService;
        private IWebHostEnvironment _webHostEnvironment;
        public BlogController(IWebHostEnvironment webHostEnvironment,ICommentService commentService, ICategoryBlogService categoryBlogService, UserManager<AppUser> userManager, IMapper mapper, IBlogService blogService, ICategoryService categoryService)
        {
            _mapper = mapper;
            _blogService = blogService;
            _categoryService = categoryService;
            _userManager = userManager;
            _categoryBlogService = categoryBlogService;
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "blog", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            var blogs = _mapper.Map<List<BlogListDto>>(_blogService.GetBlogsWithCategories());
            return View(blogs);
        }

        public IActionResult CreateOrUpdateBlog(int? blogId)
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "blog", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            if (blogId != null)
            {
                var updatedBlog = _mapper.Map<BlogCreateOrUpdateDto>(_blogService.GetById((int)blogId));
                return View(updatedBlog);
            }
            else
            {
                return View(new BlogCreateOrUpdateDto());
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBlog(BlogCreateOrUpdateDto blogCreateOrUpdate, IFormFile resim)
        {
            if (ModelState.IsValid)
            {
                blogCreateOrUpdate.AppUserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                if (resim != null)
                {
                    string uzanti = Path.GetExtension(resim.FileName);
                    string resimAd = Guid.NewGuid() + uzanti;
                    //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + resimAd);
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/" + resimAd);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await resim.CopyToAsync(stream);
                    }
                    blogCreateOrUpdate.ImagePath = resimAd;
                }
                if (blogCreateOrUpdate.Id == 0)
                {
                    if (blogCreateOrUpdate.ImagePath != null)
                    {
                        blogCreateOrUpdate.ViewsNumber = 0;
                        _blogService.Add(_mapper.Map<Blog>(blogCreateOrUpdate));
                    }
                    else
                    {
                        TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "blog", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
                        ModelState.AddModelError("", "Lütfen Resim Ekleyin");
                        return View(blogCreateOrUpdate);
                    }
                }
                else
                {
                    _blogService.Update(_mapper.Map<Blog>(blogCreateOrUpdate));
                }
                return RedirectToAction("Index");
            }
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "blog", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            return View(blogCreateOrUpdate);
        }

        [HttpPost]
        public bool RemoveBlog(int id)
        {
            try
            {
                var blog = _blogService.GetById(id);
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/" + blog.ImagePath);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                var description = blog.Description;
                var descPieces = description.Split("<img src=\"/uploads/");
                foreach (var descPiece in descPieces)
                {
                    if (descPiece.Length >= 12)
                    {
                        var descImage = descPiece.Substring(0, 12);
                        path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/" + descImage);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                }
                _blogService.Remove(blog);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        [Produces("application/json")]
        public async Task<IActionResult> UploadBlogDescImage(List<IFormFile> files)
        {
            // Get the file from the POST request
            var theFile = HttpContext.Request.Form.Files.GetFile("file");

            // Get the server path, wwwroot
            string webRootPath = _webHostEnvironment.WebRootPath;

            // Building the path to the uploads directory
            var fileRoute = Path.Combine(webRootPath, "uploads");

            // Get the mime type
            var mimeType = HttpContext.Request.Form.Files.GetFile("file").ContentType;

            // Get File Extension
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

            // Build the full path inclunding the file name
            string link = Path.Combine(fileRoute, name);

            // Create directory if it does not exist.
            FileInfo dir = new FileInfo(fileRoute);
            dir.Directory.Create();

            // Basic validation on mime types and file extension
            string[] imageMimetypes = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" };
            string[] imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };

            try
            {
                if (Array.IndexOf(imageMimetypes, mimeType) >= 0 && (Array.IndexOf(imageExt, extension) >= 0))
                {
                    // Copy contents to memory stream.
                    Stream stream;
                    stream = new MemoryStream();
                    theFile.CopyTo(stream);
                    stream.Position = 0;
                    String serverPath = link;

                    // Save the file
                    using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                    {
                        await stream.CopyToAsync(writerFileStream);
                        writerFileStream.Dispose();
                    }

                    // Return the file path as json
                    Hashtable imageUrl = new Hashtable();
                    imageUrl.Add("link", "/uploads/" + name);

                    return Json(imageUrl);
                }
                throw new ArgumentException("The image did not pass the validation");
            }

            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }
        }


        public IActionResult AssignCategory(int blogId)
        {
            TempData["Active"] = new ActiveInfoModel() { ActiveNavItem = "blog", CommentNotificationNumber = _commentService.GetUnconfirmedCommentsCount() };
            var categories = _categoryService.GetAll();
            var blogCategories = _categoryService.GetBlogCategoriesByBlogId(blogId);
            TempData["blogId"] = blogId;
            List<AssignCategoryModel> assignCategoryList = new List<AssignCategoryModel>();
            foreach (var category in categories)
            {
                AssignCategoryModel model = new AssignCategoryModel();
                model.CategoryId = category.Id;
                model.CategoryName = category.Name;
                model.Exists = blogCategories.Contains(category);
                assignCategoryList.Add(model);
            }
            return View(assignCategoryList);
        }

        [HttpPost]
        public IActionResult AssignCategory(List<AssignCategoryModel> categoryList)
        {
            int id = (int)TempData["blogId"];
            foreach (var item in categoryList)
            {
                CategoryBlogAddDto categoryBlogAddDto = new CategoryBlogAddDto()
                {
                    BlogId = id,
                    CategoryId = item.CategoryId
                };
                if (item.Exists)
                {
                    _categoryBlogService.AddToCategory(_mapper.Map<CategoryBlog>(categoryBlogAddDto));
                }
                else
                {
                    _categoryBlogService.RemoveFromCategory(_mapper.Map<CategoryBlog>(categoryBlogAddDto));
                }
            }
            return RedirectToAction("Index");
        }


    }
}
