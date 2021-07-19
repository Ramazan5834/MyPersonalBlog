using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using MyPersonalBlog.Business.Interfaces;

namespace MyPersonalBlog.UI.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ICustomLogger _customLogger;
        public ErrorController(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        //Bulunamayan Sayfa
        public IActionResult StatusCode(int? code)
        {
            if (code == 404)
            {
                ViewBag.Code = code;
                ViewBag.Message = "Sayfa Bulunamadı";
            }
            return View();
        }

        //Bulunamayan Id vs Hatalar
        public IActionResult ErrorPage()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _customLogger.LogError($"Hatanın oluştuğu yer:{exceptionHandler.Path}\nHatanın mesajı:{exceptionHandler.Error.Message}\nStack Trace:{exceptionHandler.Error.StackTrace}");
            ViewBag.Path = exceptionHandler.Path;
            ViewBag.Message = exceptionHandler.Error.Message;
            return View();
        }

    }
}
