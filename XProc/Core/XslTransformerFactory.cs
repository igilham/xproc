using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGilham.Xproc.Core
{
    /// <summary>
    /// A static factory class for acquiring a XSL transformer.
    /// </summary>
    public static class XslTransformerFactory
    {
        /// <summary>
        /// Get a transformer.
        /// </summary>
        /// <returns>A new instance of the default transformer.</returns>
        public static IXslTransformer GetTransformer()
        {
            return new ClrXslTransform();
        }
    }
}
