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
    /// Simple batch processor for XSL transforms
    /// </summary>
    public class Batcher
    {
        private readonly DirectoryInfo input_;
        private readonly DirectoryInfo output_;
        private readonly string stylesheet_;

        /// <summary>
        /// Get the input directory
        /// </summary>
        public DirectoryInfo Input
        {
            get { return input_; }
        }

        /// <summary>
        /// Get the output directory
        /// </summary>
        public DirectoryInfo Output
        {
            get { return output_; }
        }

        public string Stylesheet
        {
            get { return stylesheet_; }
        }

        /// <summary>
        /// Create a new batcher
        /// </summary>
        /// <param name="input">Input Directory</param>
        /// <param name="output">Output directory</param>
        /// <param name="stylesheetUri">URI to the stylesheet</param>
        public Batcher(DirectoryInfo input, DirectoryInfo output, string stylesheetUri)
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
            var transform = XslTransformerFactory.GetTransform();
            transform.Load(stylesheet_);
            // TODO: implement a concurrent batching strategy
            foreach (var item in inFiles)
            {
                var outPath = Path.Combine(Output.FullName, item.Name);
                transform.Transform(item.FullName, outPath);
            }
        }

    }
}
