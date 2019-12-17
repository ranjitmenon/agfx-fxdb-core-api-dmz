using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SynetecLogger
{
    /// <summary>
    /// Wrapper for nlog logger. Please implement methods from nlog should you need them.
    /// </summary>
    public class NLogWrapper : ILogWrapper
    {
        private readonly ILogger _logger;
        public NLogWrapper(string connectionString)
        {
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            _logger = NLogBuilder.ConfigureNLog(Path.Combine(Directory.GetCurrentDirectory(), "nlog.config")).GetCurrentClassLogger();
            //LogManager.Configuration.Variables["connectionString"] = connectionString;
            GlobalDiagnosticsContext.Set("connectionString", connectionString);
        }

        /// <summary>
        /// Logs to database depending on connection passed in.
        /// Please see nlog.config database configuration for table structure. (FXDBLogDB1 is an example.)
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            _logger.Error(ex, ex.Message);
        }

        public void Info(string info)
        {
            _logger.Info(info);
        }

        public void Fatal(Exception ex)
        {
            _logger.Fatal(ex, ex.Message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }
    }
}
