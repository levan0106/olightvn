using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace T.Code.Common
{
    public static class LogManager
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(LogManager));
        public static void Configurator()
        {
            XmlConfigurator.Configure();
        }
        public static void LogDebug(object ex)
        {
            log.Debug(ex);
        }
        public static void LogError(object ex)
        {
            log.Error(ex);
        }

        public static void LogWarning(object ex)
        {
            log.Warn(ex);
        }

        public static void LogInfo(object ex)
        {
            log.Info(ex);
        }

        public static void LogFatal(object ex)
        {
            log.Fatal(ex);
        }
    }
}