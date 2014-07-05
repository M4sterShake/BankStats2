using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace BankStats
{
    public static class Logger
    {
        private static string LogPath           = "BankStatsLog.txt";
        private static LogLevel Verbosity       = LogLevel.Warning;
        private static LogLevel DefaultSeverity = LogLevel.Critical;
        private static bool TraceToConsole      = true;
        private static bool Initialized         = false;
        private static StreamWriter LogWriter;

        public static void Setup(string logFilePath)
        {
            LogPath = logFilePath;
        }

        public static void Setup(string logFilePath, LogLevel verbosity)
        {
            LogPath = logFilePath;
            Verbosity = verbosity;
        }

        public static void Log(string message)
        {
            Log(message, DefaultSeverity);
        }

        public static void Log(string message, LogLevel severity)
        {
            string logMessage = string.Format("[{0}] {1} : {2}",
                                              DateTime.Now.ToString(),
                                              Enum.GetName(typeof(LogLevel), severity),
                                              message);
            try
            {
                using (LogWriter = new StreamWriter(LogPath, true))
                {
                    LogWriter.WriteLine(logMessage);
                }
            }
            catch (Exception) {}

            if (TraceToConsole)
            {
                Trace.TraceInformation(message);
            }
        }
    }

    public enum LogLevel
    {
        None        = 0,
        Critical    = 1,
        Warning     = 2,
        Information = 3,
        Debug       = 4
    }
}