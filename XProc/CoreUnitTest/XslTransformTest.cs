using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IGilham.XProc;
using NUnit.Framework;
using IGilham.XProc.Core;

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
            var tran = XslTransformerFactory.GetTransformer();
            tran.Load(Path.Combine(TestUtilities.TestData.FullName, "blank.xsl"));
            var input = new FileInfo(Path.Combine(TestUtilities.Input.FullName, "hello.xml"));
            var result = TestUtilities.GetTempFile("xml");
            tran.Transform(input.FullName, result.FullName);
            Assert.AreEqual(input.Length, result.Length, "File lengths differ");
            var expectedText = TestUtilities.GetText(input);
            var resultText = TestUtilities.GetText(result);
            Assert.AreEqual(expectedText, resultText, "File text contents differ");
        }
    }
}
