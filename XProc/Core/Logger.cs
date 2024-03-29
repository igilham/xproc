﻿using System;

namespace IGilham.XProc.Core
{
    /// <summary>
    /// Log Level indicator used by the logger
    /// </summary>
    public enum LogLevel : byte
    {
        /// <summary>
        /// Indicates that the logger is disabled.
        /// </summary>
        None = 0,

        /// <summary>
        /// Log only errors.
        /// </summary>
        Error = 1,

        /// <summary>
        /// Log only errors and warnings.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Log only errors, warnings and informational messages.
        /// </summary>
        Info = 3,

        /// <summary>
        /// Log all errors, warnings and informational messages in addition to debug messages.
        /// </summary>
        Debug = 4
    }

    /// <summary>
    /// A simple console logger.
    /// </summary>
    /// <remarks>I rolled my own to save time fiddling with a framework.
    /// Besides, the problem scope does not require anything clever.</remarks>
    public class Logger
    {
#if DEBUG
        private LogLevel level_ = LogLevel.Debug;
#else
        private LogLevel level_ = LogLevel.Warning;
#endif

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// This is marked internal to prevent the logger from being instantiated by 
        /// client applications. Use LoggerService to acquire a Logger
        /// </remarks>
        internal Logger() { }

        #region public logger API

        /// <summary>
        /// Get or set the log level
        /// </summary>
        public LogLevel Level
        {
            get { return level_; }
            set { level_ = value; }
        }

        /// <summary>
        /// Log that an error has occurred
        /// </summary>
        /// <param name="message">The message to write out.</param>
        public virtual void Error(string message)
        {
            const string prefix = "ERROR: ";
            if (Level >= LogLevel.Error)
            {
                Log(prefix, message);
            }
        }

        /// <summary>
        /// Log a warning
        /// </summary>
        /// <param name="message">The message to write out.</param>
        public virtual void Warning(string message)
        {
            const string prefix = "WARNING: ";
            if (Level >= LogLevel.Warning)
            {
                Log(prefix, message);
            }
        }

        /// <summary>
        /// Log an informational message
        /// </summary>
        /// <param name="message">The message to write out.</param>
        public virtual void Info(string message)
        {
            const string prefix = "INFO: ";
            if (Level >= LogLevel.Info)
            {
                Log(prefix, message);
            }
        }

        /// <summary>
        /// Log a debug message to the console
        /// </summary>
        /// <param name="message">The message to write out.</param>
        public virtual void Debug(string message)
        {
            const string prefix = "DEBUG: ";
            if (Level >= LogLevel.Debug)
            {
                Log(prefix, message);
            }
        }

        #endregion

        private void Log(string prefix, string message)
        {
            Console.Error.WriteLine(string.Concat(prefix, message));
        }
    }
}
