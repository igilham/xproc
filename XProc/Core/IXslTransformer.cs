using System;

namespace IGilham.XProc.Core
{
    public interface IXslTransformer
    {
        /// <summary>
        /// Transform inPath to outPath using the stylesheet.
        /// </summary>
        /// <param name="inPath">Input file path</param>
        /// <param name="outPath">Output file path</param>
        /// <exception cref="XslTransformException">If there is an error processing the file.</exception>
        void Transform(string inPath, string outPath);
    }
}
