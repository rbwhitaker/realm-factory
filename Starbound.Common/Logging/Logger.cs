using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Starbound.Common.Logging
{
    public static class Logger
    {
        public static LogLevel LogLevel { get; set; }

        static Logger()
        {
            LogLevel = Logging.LogLevel.Trace;
        }

        public static void Log(LogLevel logLevel, string message)
        {
            if (logLevel < LogLevel) { return; }

            string dateString = new DateTime().ToString("dd MMM yyyy  HH:mm:ss.FFF");
            string fullText = dateString + "  --  " + logLevel.ToString() + "  --  " + message;
            File.AppendAllText("./log.txt", fullText);
        }

        public static void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        public static void Warning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public static void Info(string message)
        {
            Log(LogLevel.Info, message);
        }

        public static void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public static void Trace(string message)
        {
            Log(LogLevel.Trace, message);
        }
    }
}
