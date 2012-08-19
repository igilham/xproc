using System;
using System.IO;
using System.Xml.Xsl;

namespace IGilham.XProc.Core
{
    /// <summary>
    /// A simple class wrapping the standard .Net library XSL transform API.
    /// </summary>
    public class ClrXslTransformer : IXslTransformer
    {
        private XslCompiledTransform transform_ = new XslCompiledTransform();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stylesheet">The XSL stylesheet to use for transforms.</param>
        /// <remarks>
        /// stylesheet is set in the constructor to ensure that it does not
        /// lead to race conditions
        /// </remarks>
        /// <exception cref="XslLoadException">If the given file cannot be loaded.</exception>
        public ClrXslTransformer(FileInfo stylesheet)
        {
            var log = LoggerService.GetLogger();
            try
            {
                transform_.Load(stylesheet.FullName);
                log.Debug(string.Concat("Stylesheet loaded from path: ", stylesheet));
            }
            catch (Exception ex)
            {
                var message = string.Concat("Error loading stylesheet from: ", stylesheet.FullName);
                throw new XslLoadException(message, ex, stylesheet.FullName);
            }
        }

        /// <summary>
        /// Transform inPath to outPath using the stylesheet.
        /// </summary>
        /// <param name="inPath">Input file path</param>
        /// <param name="outPath">Output file path</param>
        /// <exception cref="XslTransformException">If there is an error processing the file.</exception>
        public void Transform(string inPath, string outPath)
        {
            var log = LoggerService.GetLogger();
            try
            {
                transform_.Transform(inPath, outPath);
                log.Debug(string.Format("Successfully transformed {0} to {1}", inPath, outPath));
            }
            catch (Exception ex)
            {
                var message = string.Format("Error transforming XML. inPath={0}, outPath={1}", inPath, outPath);
                throw new XslTransformException(message, ex, inPath, outPath);
            }
        }
    }
}
