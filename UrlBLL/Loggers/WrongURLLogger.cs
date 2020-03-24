using System;
using NLog;
using BLL.Interfaces;

namespace UrlBLL
{
    public class WrongURLLogger : BLL.Interfaces.ILogger
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public WrongURLLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logfile.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            
            NLog.LogManager.Configuration = config;
        }

        public void LogError(string message)
        {
            this.logger.Error(message);
        }
    }
}
