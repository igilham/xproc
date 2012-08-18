using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace IGilham.Xproc.Core
{
    /// <summary>
    /// A simple class wrapping the standard .Net library XSL transform API.
    /// </summary>
    public class ClrXslTransform : IXslTransformer
    {
        private XslCompiledTransform transform_ = new XslCompiledTransform();

        /// <summary>
        /// Load a stylesheet into the transformer.
        /// </summary>
        /// <param name="stylesheetUri">The URI string pointing to the file path of the XSL stylesheet.</param>
        /// <exception cref="XslLoadException">If the given file cannot be loaded.</exception>
        public void Load(string stylesheetUri)
        {
            var log = LoggerService.GetLogger();
            try
            {
                transform_.Load(stylesheetUri);
                log.Debug(string.Concat("Stylesheet loaded from path: ", stylesheetUri));
            }
            catch (Exception ex)
            {
                var message = string.Concat("Error loading stylesheet from: ", stylesheetUri);
                throw new XslLoadException(message, ex, stylesheetUri);
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
