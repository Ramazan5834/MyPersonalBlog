using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using NLog;

namespace MyPersonalBlog.UI.CustomCollectionExtensions
{
    public static class NLogConfiguration
    {
        private static IWebHostEnvironment _webHostEnvironment;
        public static void ConfigurationForNLog(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            var appBasePath = _webHostEnvironment.WebRootPath;
            NLog.GlobalDiagnosticsContext.Set("appbasepath", appBasePath);
            var logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
        }
    }
}
