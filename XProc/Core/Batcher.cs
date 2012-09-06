using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IGilham.XProc.Core
{
    /// <summary>A delegate type for hooking up batch completion event handlers.</summary>
    public delegate void BatchCompletedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Batch processor for XSL transforms. Synchronous and asynchronous methods are available.
    /// </summary>
    /// <remarks>
    /// Batch processing may use multiple threads as determined by the .Net framework.
    /// </remarks>
    public class Batcher
    {
        /// <summary>
        /// An event fired when the batch job launched by calling ProcessBatchAsync without a
        /// callback function completes.
        /// </summary>
        public event BatchCompletedEventHandler BatchCompleted;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>
        /// The IXslTransformer dependency is injected to make it easier to
        /// switch XSLT library. The transformer is immutable.
        /// </remarks>
        /// <param name="transformer">Transformer to use for batch processing.</param>
        public Batcher(IXslTransformer transformer)
        {
            this.transformer_ = transformer;
        }

        /// <summary>
        /// Batch process files using an XSL transformer.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to use for the batch transform.</param>
        /// <param name="outputDir">The directory in which to place the transformed files.</param>
        /// <param name="files">The files to transform.</param>
        public void ProcessBatch(DirectoryInfo outputDir, IEnumerable<FileInfo> files)
        {
            log_.Debug("Processbatch() called synchronously");
            SetUpBatchJob(outputDir, files);
            ProcessBatchFiles(outputDir, files);
        }

        /// <summary>
        /// Asynchronously batch process files using an XSL transformer. The BatchCompleted event will be
        /// raised to notify completion.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to use for the batch transform.</param>
        /// <param name="outputDir">The directory in which to place the transformed files.</param>
        /// <param name="files">The files to transform.</param>
        public void ProcessBatchAsync(DirectoryInfo outputDir, IEnumerable<FileInfo> files)
        {
            log_.Debug("ProcessBatchAsync() called using local event callback");
            RunBatch run = ProcessBatch;
            run.BeginInvoke(outputDir, files, BatchProcessCallback, null);
        }

        /// <summary>
        /// Asynchronously batch process files using an XSL transformer. The user-supplied callback method will 
        /// be called on batch job completion.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to use for the batch transform.</param>
        /// <param name="outputDir">The directory in which to place the transformed files.</param>
        /// <param name="files">The files to transform.</param>
        /// <param name="callback">User supplied callback function to receive batch completion notification.</param>
        public void ProcessBatchAsync(DirectoryInfo outputDir, IEnumerable<FileInfo> files,
            AsyncCallback callback)
        {
            log_.Debug("ProcessBatchAsync() called with user-suplied callback");
            RunBatch run = ProcessBatch;
            if (callback == null)
            {
                log_.Warning("user-suplied callback is null, running batch as fire-and-forget");
            }
            run.BeginInvoke(outputDir, files, callback, null);
        }

        protected virtual void OnBatchCompleted()
        {
            // take a copy to prevent NullReferenceException in case of event unsubscription.
            var handler = BatchCompleted;
            if (handler != null)
            {
                log_.Debug("Firing BatchCompleted event");
                handler(this, new EventArgs());
            }
            else
            {
                log_.Warning("Batch completed but no event handlers were registered");
            }
        }

        #region private helper methods

        private void SetUpBatchJob(DirectoryInfo outputDir, IEnumerable<FileInfo> files)
        {
            if (!outputDir.Exists)
            {
                log_.Debug(string.Concat("Creating output directory: ", outputDir.FullName));
                try
                {
                    outputDir.Create();
                }
                catch (IOException)
                {
                    log_.Error("IOException thrown when creating output directory");
                    throw;
                }
            }
        }

        /// <summary>
        /// This method performs the actual batch processing.
        /// </summary>
        /// <remarks>
        /// Batch file processing is embarrasingly parallel so the simple Parallel.ForEach 
        /// API should be able to do a better job than I can in much less code.
        /// </remarks>
        /// <param name="outputDir">The output directory, assumed to exist.</param>
        /// <param name="files">The files to batch process.</param>
        private void ProcessBatchFiles(DirectoryInfo outputDir, IEnumerable<FileInfo> files)
        {
            var result = Parallel.ForEach(files, currentFile =>
            {
                var outPath = Path.Combine(outputDir.FullName, currentFile.Name);
                var targetFile = new FileInfo(outPath);
                try
                {
                    log_.Debug(string.Concat("Transforming ", currentFile.FullName, " to ", outPath));
                    transformer_.Transform(currentFile, targetFile);
                }
                catch (XProcException e)
                {
                    log_.Error(e.Message);
                }
            });
            // wait for processing to finish
            while (!result.IsCompleted)
            {
                Thread.Sleep(150);
            }
        }

        /// <summary>
        /// Callback method called when an asynchronous batch job finishes.
        /// </summary>
        /// <param name="result">The result of the batch job.</param>
        private void BatchProcessCallback(IAsyncResult result)
        {
            log_.Debug("Batch completion notified via local callback");
            OnBatchCompleted();
        }

        #endregion

        private readonly IXslTransformer transformer_;
        private static Logger log_ = LoggerService.GetLogger();

        /// <summary>
        /// Delegate to call the ProcessBatch method asynchronously.
        /// </summary>
        /// <param name="stylesheet">Stylesheet used to transform files.</param>
        /// <param name="outputDir">Output directory.</param>
        /// <param name="files">Files to transform.</param>
        private delegate void RunBatch(DirectoryInfo outputDir, IEnumerable<FileInfo> files);
    }
}
