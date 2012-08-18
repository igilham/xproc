using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IGilham.Xproc.Core
{
    /// <summary>
    /// The base exception class in XProc.
    /// </summary>
    [Serializable]
    public class XProcException : Exception
    {
        public XProcException() { }
        public XProcException( string message ) : base( message ) { }
        public XProcException( string message, Exception inner ) : base( message, inner ) { }
        protected XProcException(SerializationInfo info, StreamingContext context ) : base( info, context ) { }
    }

    /// <summary>
    /// An XslException that may occur while trying to load an XSL stylesheet into a transformer.
    /// </summary>
    [Serializable]
    public class XslLoadException : XProcException
    {
        public XslLoadException() { }
        public XslLoadException(string message) : base(message) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="xslFilePath">The path of the XSL stylesheet.</param>
        public XslLoadException(string message, string xslFilePath) : base(message) { xslFilePath_ = xslFilePath; }

        public XslLoadException(string message, Exception inner) : base(message, inner) { }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner Exception</param>
        /// <param name="xslFilePath">The path of the XSL stylesheet.</param>
        public XslLoadException(string message, Exception inner, string xslFilePath) : base(message, inner)
        {
            xslFilePath_ = xslFilePath;
        }

        protected XslLoadException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <param name="xslFilePath">The path of the XSL stylesheet.</param>
        protected XslLoadException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context,
            string xslFilePath)
            : base(info, context)
        {
            xslFilePath_ = xslFilePath;
        }

        public string XslFilePath { get { return xslFilePath_; } }

        private string xslFilePath_;
    }

    /// <summary>
    /// An exception that may be thown while trying to transform an XML file with an XSL stylesheet.
    /// </summary>
    [Serializable]
    public class XslTransformException : XProcException
    {
        public XslTransformException() { }

        public XslTransformException(string message) : base(message) { }

        public XslTransformException(string message, string inPath, string outPath) : base(message)
        {
            inputPath_ = inPath;
            outputPath_ = outPath;
        }

        public XslTransformException(string message, Exception inner) : base(message, inner) { }

        public XslTransformException(string message, Exception inner, string inPath, string outPath)
            : base(message, inner)
        {
            inputPath_ = inPath;
            outputPath_ = outPath;
        }

        protected XslTransformException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

        protected XslTransformException(SerializationInfo info, StreamingContext context, 
            string inPath, string outPath)
            : base(info, context)
        {
            inputPath_ = inPath;
            outputPath_ = outPath;
        }

        /// <summary>
        /// The input path of the transform.
        /// </summary>
        public string InputPath { get { return inputPath_; } }

        /// <summary>
        /// The output path of the transform.
        /// </summary>
        public string OutputPath { get { return outputPath_; } }

        private string inputPath_;
        private string outputPath_;
    }
}
