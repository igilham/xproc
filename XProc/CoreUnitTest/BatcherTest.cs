using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IGilham.XProc.Core;
using System.IO;

namespace IGilham.XProc.UnitTest
{
    /// <remarks>
    /// Beware the bizzare case of directories not being deleted 
    /// when you call DirectoryInfo.Delete(). See the remarks on
    /// DeleteOutputDir(), below.
    /// </remarks>
    [TestFixture]
    public class BatcherTest
    {
        [Test]
        public void EnsureOutputDirectoryCreated()
        {
            DeleteOutputDir();
            Assert.IsFalse(TestUtilities.Output.Exists, "Output directory exists");
            var batcher = new Batcher();
            batcher.ProcessBatch(TestUtilities.CatalogXsl, TestUtilities.Output, TestUtilities.Catalog.EnumerateFiles());
            Assert.That(TestUtilities.Catalog.Exists, "Output directory not created");
        }

        [Test]
        public void EnsureSameOutputFileCount()
        {
            DeleteOutputDir();
            var inFiles = TestUtilities.Catalog.EnumerateFiles("*.xml");
            Assert.IsNotEmpty(inFiles, "No catalog xml files found");
            var batcher = new Batcher();
            batcher.ProcessBatch(TestUtilities.CatalogXsl, TestUtilities.Output, inFiles);
            var outFiles = TestUtilities.Output.EnumerateFiles("*.xml");
            Assert.AreEqual(inFiles.Count(),  outFiles.Count(),
                "Input and output file counts differ");
        }

        /// <summary>
        /// Delete the output directory.
        /// </summary>
        /// <remarks>
        /// For some inexplicable reason, this doesn't always work
        /// see (http://manfredlange.blogspot.co.uk/2011/11/behavior-of-directoryinfodelete-and.html)
        /// </remarks>
        private void DeleteOutputDir()
        {
            if (TestUtilities.Output.Exists)
            {
                Directory.Delete(TestUtilities.Output.FullName, true);
            }
        }
    }
}
