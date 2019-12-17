using log4net.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SynetecLogger
{
    /// <summary>
    /// Generic logger. Logs depending on implementation. Please see individual classes.
    /// </summary>
    public interface ILogWrapper
    {
        void Error(Exception ex);
        void Info(string info);
        void Fatal(Exception ex);
        void Debug(string message);
    }
}
