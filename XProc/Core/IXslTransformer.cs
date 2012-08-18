using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.XPath;

namespace IGilham.Xproc.Core
{
    public interface IXslTransformer
    {
        /// <summary>
        /// Load a stylesheet into the transformer.
        /// </summary>
        /// <param name="stylesheetUri">The URI string pointing to the file path of the XSL stylesheet.</param>
        void Load(string stylesheetUri);

        /// <summary>
        /// Transform inPath to outPath using the stylesheet.
        /// </summary>
        /// <param name="inPath">Input file path</param>
        /// <param name="outPath">Output file path</param>
        void Transform(string inPath, string outPath);
    }
}
