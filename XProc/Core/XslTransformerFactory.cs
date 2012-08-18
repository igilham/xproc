using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGilham.Xproc.Core
{
    public static class XslTransformerFactory
    {
        public static IXslTransformer GetTransform()
        {
            return new ClrXslTransform();
        }
    }
}
