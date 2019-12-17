using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace SynetecLogger
{
    /// <summary>
    /// This is log4net wrapper which log to file to executing assembly location.
    /// Pass in fileName and/or hosting directory which will be created inside current directory.
    /// </summary>
    public class Log4NetWrapper : ILogWrapper
    {
        private static readonly ILog _logger =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool _disposed;

        private FileAppender appender;

        public Log4NetWrapper(string fileName)
        {
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "log4net.config")));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            appender = (log4net.Appender.RollingFileAppender)repo.GetAppenders()[0];
            appender.File = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            //appender.ActivateOptions();
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Logs exception to the file and shuts down repository, i.e. does not lock a file.
        /// Suitable for singleton class, but for any other class/project.
        /// If you need disposable (scoped) please implement the same interface with the same code
        /// and activate logger in constructor and shut down repository in Dispose() as commented out code below.
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            ActivateLogger();
            if (_logger.IsErrorEnabled)
            {
                var exception = ex;
                _logger.Error(exception.Message, exception);
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    _logger.Error(exception.Message, exception);
                }
            }
            _logger.Logger.Repository.Shutdown();
        }

        public void Fatal(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Info(string info)
        {
            throw new NotImplementedException();
        }

        private void ActivateLogger()
        {
            appender.ActivateOptions();
        }

        //public ILogger GetLogger()
        //{
        //    return _logger.Logger;
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _logger.Logger.Repository.Shutdown();
        //        }
        //    }
        //    _disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
