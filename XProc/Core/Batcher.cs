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
        /// <summary>
        /// Batch process files using an XSL transformer.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to use for the batch transform.</param>
        /// <param name="outputDir">The directory in which to place the transformed files.</param>
        /// <param name="files">The files to transform.</param>
        public void ProcessBatch(FileInfo stylesheet, DirectoryInfo outputDir, IEnumerable<FileInfo> files)
        {
            var log = LoggerService.GetLogger();
            if (!outputDir.Exists)
            {
                log.Debug(string.Format("Creating output directory: {}", outputDir.FullName));
                try
                {
                    outputDir.Create();
                }
                catch (IOException)
                {
                    log.Error("IOException thrown when creating output directory");
                    throw;
                }
            }
            var transform = XslTransformerFactory.GetTransformer();
            try
            {
                transform.Load(stylesheet.FullName);
            }
            catch (XslLoadException e)
            {
                log.Error(e.Message);
                throw;
            }

            // Batch file processing is embarrasingly parallel so the simple
            // Parallel.ForEach API should be able to do a better job 
            // than I can in much less code.
            Parallel.ForEach(files, currentFile =>
            {
                var outPath = Path.Combine(outputDir.FullName, currentFile.Name);
                try
                {
                    transform.Transform(currentFile.FullName, outPath);
                }
                catch (XProcException e)
                {
                    LoggerService.GetLogger().Error(e.Message);
                }
            });
        }

    }
}
