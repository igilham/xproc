using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IGilham.XProc;
using NUnit.Framework;
using IGilham.Xproc.Core;

namespace IGilham.XProc.UnitTest
{
    [TestFixture]
    public class XslTransformTest
    {

        [Test]
        public void EnsureTestDataAvailable()
        {
            Assert.IsTrue(TestUtilities.TestData.Exists);
            Assert.IsTrue(TestUtilities.Input.Exists);
            Assert.IsTrue(TestUtilities.Catalog.Exists);
        }

        [Test]
        public void EnsureBlankStylesheetDoesNothing()
        {
            var tran = XslTransformerFactory.GetTransformer();
            tran.Load(Path.Combine(TestUtilities.TestData.FullName, "blank.xsl"));
            var input = new FileInfo(Path.Combine(TestUtilities.Input.FullName, "hello.xml"));
            var result = TestUtilities.GetTempFile("xml");
            tran.Transform(input.FullName, result.FullName);
            Assert.AreEqual(input.Length, result.Length);
            var expectedText = TestUtilities.GetText(input);
            var resultText = TestUtilities.GetText(result);
            Assert.AreEqual(expectedText, resultText);
        }
    }
}
