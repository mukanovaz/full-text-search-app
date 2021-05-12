using System;

namespace FullTextSearch.SimpleLogger
{
    public static class Logger
    {
        /// <summary>
        /// The datetime format used in logs
        /// </summary>
        private const string DATETIME_FORMAT = "HH:mm:ss dd-MM-yyyy";
        /// <summary>
        /// The filename where the logs are saved
        /// </summary>
        //public static string LOG_FILE = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), ".log");
        /// <summary>
        /// The variable where the user can turn on/off console output
        /// </summary>
        private static bool consoleOutput = true;

        /// <summary>
        /// Initialize a new instance of SimpleLogger class.
        /// Log file will be created automatically if not yet exists, else it can be either a fresh new file or append to the existing file.
        /// Default is create a fresh new log file.
        /// </summary>
        /// <param name="append">True to append to existing log file, False to overwrite and create new log file</param>
        static Logger()
        {
        }

        /// <summary>
        /// Set console output according to parameter
        /// </summary>
        /// <param name="set">if is set parameter true console will be
        /// turn on otherwise won't</param>
        public static void ConsoleOutput(bool set)
        {
            consoleOutput = set;
        }

        /// <summary>
        /// Log a debug message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Debug(string text)
        {
            WriteFormattedLog(LogLevel.DEBUG, text);
        }

        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Error(string text)
        {
            WriteFormattedLog(LogLevel.ERROR, text);
        }

        /// <summary>
        /// Log a fatal error message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Fatal(string text)
        {
            WriteFormattedLog(LogLevel.FATAL, text);
        }

        /// <summary>
        /// Log an info message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Info(string text)
        {
            WriteFormattedLog(LogLevel.INFO, text);
        }

        /// <summary>
        /// Log a trace message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Trace(string text)
        {
            WriteFormattedLog(LogLevel.TRACE, text);
        }

        /// <summary>
        /// Log a waning message
        /// </summary>
        /// <param name="text">Message</param>
        public static void Warning(string text)
        {
            WriteFormattedLog(LogLevel.WARNING, text);
        }

        /// <summary>
        /// Format a log message based on log level
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="text">Log message</param>
        private static void WriteFormattedLog(LogLevel level, string text)
        {
            string str;
            switch (level)
            {
                case LogLevel.TRACE:
                    str = DateTime.Now.ToString(DATETIME_FORMAT) + " [TRACE] ";
                    break;

                case LogLevel.INFO:
                    str = DateTime.Now.ToString(DATETIME_FORMAT) + " [INFO] ";
                    break;

                case LogLevel.DEBUG:
                    str = DateTime.Now.ToString(DATETIME_FORMAT) + " [DEBUG] ";
                    break;

                case LogLevel.WARNING:
                    str = DateTime.Now.ToString(DATETIME_FORMAT) + " [WARNING] ";
                    break;

                case LogLevel.ERROR:
                    str = DateTime.Now.ToString(DATETIME_FORMAT) + " [ERROR] ";
                    break;

                case LogLevel.FATAL:
                    str = DateTime.Now.ToString(DATETIME_FORMAT) + " [FATAL] ";
                    break;

                default:
                    str = "";
                    break;
            }
            WriteLine(str + text, true);
        }

        /// <summary>
        /// Write a line of formatted log message into a log file
        /// </summary>
        /// <param name="text">Formatted log message</param>
        /// <param name="append">True to append, False to overwrite the file</param>
        /// <exception cref="System.IO.IOException"></exception>
        private static void WriteLine(string text, bool append = true)
        {
            if (consoleOutput)
            {
                Console.Write(text + "\n");
            }
        }

        /// <summary>
        /// Supported log level
        /// </summary>
        [Flags]
        private enum LogLevel
        {
            TRACE,
            INFO,
            DEBUG,
            WARNING,
            ERROR,
            FATAL
        }
    }
}
