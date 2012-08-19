using System.IO;
using IGilham.XProc.Core;
using NUnit.Framework;

namespace IGilham.XProc.UnitTest
{
    [TestFixture]
    public class XslTransformTest
    {

        [Test]
        public void EnsureTestDataAvailable()
        {
            Assert.That(TestUtilities.TestData.Exists);
            Assert.That(TestUtilities.Input.Exists);
            Assert.That(TestUtilities.Catalog.Exists);
            Assert.That(TestUtilities.CatalogXsl.Exists);
            Assert.That(TestUtilities.BlankXsl.Exists);
        }

        [Test]
        public void EnsureBlankStylesheetDoesNothing()
        {
            var tran = new ClrXslTransformer(TestUtilities.BlankXsl);
            var input = new FileInfo(Path.Combine(TestUtilities.Input.FullName, "hello.xml"));
            var result = TestUtilities.GetTempFile("xml");
            tran.Transform(input, result);
            Assert.AreEqual(input.Length, result.Length, "File lengths differ");
            var expectedText = TestUtilities.GetText(input);
            var resultText = TestUtilities.GetText(result);
            Assert.AreEqual(expectedText, resultText, "File text contents differ");
        }
    }
}
