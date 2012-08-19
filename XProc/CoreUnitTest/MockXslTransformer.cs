using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGilham.XProc.Core;

namespace IGilham.XProc.UnitTest
{
    /// <summary>
    /// Mock transformer class to verify the behaviour of the Batcher.
    /// </summary>
    public class MockXslTransformer : IXslTransformer
    {
        #region IXslTransformer
        
        public void Load(string stylesheetUri)
        {
            stylesheetLoaded_ = true;
            stylesheetUri_ = stylesheetUri;
        }

        public void Transform(string inPath, string outPath)
        {
            ++transformCalledCount_;
            files_.Add(inPath, outPath);
        }
        
        #endregion

        public bool StylesheetLoaded { get { return stylesheetLoaded_; } }

        public int TransformCalledCount { get { return transformCalledCount_; } }

        public string StylesheetUri { get { return stylesheetUri_; } }

        public Dictionary<string, string> Files { get { return files_; } }

        private bool stylesheetLoaded_ = false;
        private int transformCalledCount_ = 0;
        private string stylesheetUri_;
        private Dictionary<string, string> files_ = new Dictionary<string, string>();
    }
}
