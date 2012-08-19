using System.IO;
using System;

namespace IGilham.XProc.Core
{
    /// <summary>
    /// A very simple command line argument parser.
    /// </summary>
    /// <remarks>
    /// This is in the Core dll because of a compiler limitation. The executable cannot be compiled as
    /// platform-independent, so crossing the dll boundary would generate warnings when trying to test this class.
    /// </remarks>
    public class ArgumentParser
    {
        private DirectoryInfo inputPath_;
        private DirectoryInfo outputPath_;
        private FileInfo stylesheet_;

        /// <summary>
        /// Get the Input directory where xml files are ready to be transformed.
        /// </summary>
        public DirectoryInfo InputPath { get { return inputPath_; } }

        /// <summary>
        /// Get the Output directory.
        /// </summary>
        public DirectoryInfo OutputPath { get { return outputPath_; } }

        /// <summary>
        /// Get the stylesheet used for transforms.
        /// </summary>
        public FileInfo Stylesheet { get { return stylesheet_; } }

        /// <summary>
        /// Get the help message for the command line.
        /// </summary>
        public string HelpMessage
        {
            get
            {
                return "Usage: XProc INPUT_DIR OUTPUT_DIR STYLESHEET";
            }
        }

        /// <summary>
        /// Parse the command line.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>True if the command line is successfully parsed, false otherwise.</returns>
        public bool Parse(params string[] args)
        {
            var log = LoggerService.GetLogger();
            log.Debug(string.Concat("Parsing command line arguments", string.Join(", ", args)));
            const int expectedArguments = 3;

            if (args.Length != expectedArguments)
            {
                log.Error("Incorrect argument count while parsing the command line. Expected " + expectedArguments);
                return false;
            }
            try
            {
                inputPath_ = new DirectoryInfo(args[0]);
                if (!inputPath_.Exists)
                {
                    log.Error(string.Concat("No such directory: ", inputPath_.FullName));
                    return false;
                }
                outputPath_ = new DirectoryInfo(args[1]);
                stylesheet_ = new FileInfo(args[2]);
            }
            catch (IOException e)
            {
                log.Error(string.Concat("IOException caught while parsing the command line: ", e.Message));
                return false;
            }
            return true;
        }
    }
}
