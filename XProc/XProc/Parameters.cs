using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IGilham.XProc
{
    /// <summary>
    /// A command line argument parser.
    /// </summary>
    class Parameters
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
            if (args.Length != 4)
            {
                return false;
            }
            try
            {
                inputPath_ = new DirectoryInfo(args[1]);
                outputPath_ = new DirectoryInfo(args[2]);
                stylesheet_ = new FileInfo(args[3]);
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
    }
}
