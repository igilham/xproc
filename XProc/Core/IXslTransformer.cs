using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.XPath;

namespace IGilham.XProc.Core
{
    public interface IXslTransformer
    {
        /// <summary>
        /// Load a stylesheet into the transformer.
        /// </summary>
        /// <param name="stylesheetUri">The URI string pointing to the file path of the XSL stylesheet.</param>
        /// <exception cref="XslLoadException">If the given file cannot be loaded.</exception>
        void Load(string stylesheetUri);

        /// <summary>
        /// Transform inPath to outPath using the stylesheet.
        /// </summary>
        /// <param name="inPath">Input file path</param>
        /// <param name="outPath">Output file path</param>
        /// <exception cref="XslTransformException">If there is an error processing the file.</exception>
        void Transform(string inPath, string outPath);
    }
}
