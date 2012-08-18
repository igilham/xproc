using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Xml.XPath;

namespace IGilham.Xproc.Core
{
    /// <summary>
    /// Simple batch processor for XSL transforms.
    /// </summary>
    public class Batcher
    {
        private readonly DirectoryInfo input_;
        private readonly DirectoryInfo output_;
        private readonly FileInfo stylesheet_;

        #region public properties

        /// <summary>
        /// Get the input directory.
        /// </summary>
        public DirectoryInfo Input
        {
            get { return input_; }
        }

        /// <summary>
        /// Get the output directory.
        /// </summary>
        public DirectoryInfo Output
        {
            get { return output_; }
        }

        /// <summary>
        /// Get the stylesheet.
        /// </summary>
        public FileInfo Stylesheet
        {
            get { return stylesheet_; }
        }

        #endregion

        /// <summary>
        /// Create a new batcher
        /// </summary>
        /// <param name="input">Input Directory</param>
        /// <param name="output">Output directory</param>
        /// <param name="stylesheetUri">URI to the stylesheet</param>
        public Batcher(DirectoryInfo input, DirectoryInfo output, FileInfo stylesheetUri)
        {
            input_ = input;
            output_ = output;
            stylesheet_ = stylesheetUri;
        }

        /// <summary>
        /// batch process the files in Input and put the results in Output.
        /// </summary>
        public void ProcessBatch()
        {
            if (!Output.Exists)
            {
                Output.Create();
            }
            var inFiles = Input.EnumerateFiles("*.xml");
            var transform = XslTransformerFactory.GetTransformer();
            try
            {
                transform.Load(stylesheet_.FullName);
            }
            catch (XProcException e)
            {
                LoggerService.GetLogger().Error(e.Message);
                throw;
            }

            // The problem of processing multiple documents is embarrasingly parallel
            // so the simple Parallel.ForEach API should be able to do a better job 
            // than I can in much less code.
            Parallel.ForEach<FileInfo>(inFiles, (x) => {
                var outPath = Path.Combine(Output.FullName, x.Name);
                try
                {
                    transform.Transform(x.FullName, outPath);
                }
                catch (XProcException e)
                {
                    LoggerService.GetLogger().Error(e.Message);
                }
            });
        }

    }
}
