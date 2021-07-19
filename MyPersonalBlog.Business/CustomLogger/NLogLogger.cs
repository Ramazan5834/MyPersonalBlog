using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Business.Interfaces;
using NLog;

namespace MyPersonalBlog.Business.CustomLogger
{
    public class NLogLogger : ICustomLogger
    {
        public void LogError(string message)
        {
            var logger = LogManager.GetLogger("loggerFile");
            logger.Log(LogLevel.Error, message);
        }
    }
}
