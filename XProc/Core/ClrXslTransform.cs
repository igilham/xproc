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

        public void Load(string stylesheetUri)
        {
            transform_.Load(stylesheetUri);
        }

        public void Transform(string inPath, string outPath)
        {
            transform_.Transform(inPath, outPath);
        }
    }
}
