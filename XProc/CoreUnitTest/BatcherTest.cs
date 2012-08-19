using System.IO;
using System.Linq;
using IGilham.XProc.Core;
using NUnit.Framework;

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
        public void EnsureAllFilesProcessed()
        {
            var transformer = new MockXslTransformer(TestUtilities.CatalogXsl);
            var batcher = new Batcher(transformer);
            var files = TestUtilities.Catalog.EnumerateFiles();
            batcher.ProcessBatch(TestUtilities.Output, files);
            Assert.AreEqual(files.Count(), transformer.TransformCalledCount,
                "IXslTransformer.Transform() was not called for all input files");
            foreach (var file in files)
            {
                Assert.That(transformer.Files.ContainsKey(file.FullName), 
                    string.Concat("file not found in output: ", file.FullName));
            }
        }

        /// <remarks>
        /// This test will randomly fail. See the note on DeleteOutputDir() below.
        /// </remarks>
        [Test]
        public void EnsureOutputDirectoryCreated()
        {
            DeleteOutputDir();
            Assert.IsFalse(TestUtilities.Output.Exists, "Output directory exists");
            var batcher = new Batcher(new MockXslTransformer(TestUtilities.CatalogXsl));
            batcher.ProcessBatch(TestUtilities.Output, TestUtilities.Catalog.EnumerateFiles());
            Assert.That(TestUtilities.Catalog.Exists, "Output directory not created");
        }

        /// <remarks>
        /// This test will randomly fail. See the note on DeleteOutputDir() below.
        /// </remarks>
        [Test]
        public void EnsureSameOutputFileCount()
        {
            DeleteOutputDir();
            var inFiles = TestUtilities.Catalog.EnumerateFiles("*.xml");
            Assert.IsNotEmpty(inFiles, "No catalog xml files found");
            var batcher = new Batcher(new ClrXslTransform(TestUtilities.CatalogXsl));
            batcher.ProcessBatch(TestUtilities.Output, inFiles);
            var outFiles = TestUtilities.Output.EnumerateFiles("*.xml");
            Assert.AreEqual(inFiles.Count(),  outFiles.Count(),
                "Input and output file counts differ");
        }

        /// <summary>
        /// Delete the output directory.
        /// </summary>
        /// <remarks>
        /// For some inexplicable reason, this doesn't always work
        /// see (http://manfredlange.blogspot.co.uk/2011/11/behavior-of-directoryinfodelete-and.html).
        /// This will cause some tests above to randomly fail. Re-running them is often successful.
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
