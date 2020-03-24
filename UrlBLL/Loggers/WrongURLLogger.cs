using System;
using BLL.Interfaces;
using NLog;

namespace UrlBLL
{
    /// <summary>Class-container for the URL logger that logs errors encounted while converting URL address.</summary>
    public class WrongURLLogger
    {
        /// <summary>Initializes a new instance of the <see cref="WrongURLLogger"/> class.</summary>
        public WrongURLLogger()
        {
            this.Logger = LogManager.GetCurrentClassLogger();
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logfile.txt" };
            var logConsole = new NLog.Targets.ConsoleTarget("logConsole");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            logConsole.Dispose();
            logfile.Dispose();
            NLog.LogManager.Configuration = config;
        }

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; }
    }
}
