using System;

namespace IGilham.XProc.Core
{
    /// <summary>
    /// A static Factory class for managing access to the logger.
    /// The logger should only be accessed through this API.
    /// </summary>
    public static class LoggerService
    {
        private static Logger logger_ = new Logger();

        /// <summary>
        /// Get a logger.
        /// </summary>
        /// <returns>The Application's logger.</returns>
        public static Logger GetLogger()
        {
            return logger_;
        }
    }
}
