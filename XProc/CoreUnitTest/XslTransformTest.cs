using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IGilham.XProc;
using NUnit.Framework;
using IGilham.Xproc.Core;

namespace IGilham.XProc.CoreUnitTest
{
    [TestFixture]
    public class XslTransformTest
    {
        private static DirectoryInfo TestData = new DirectoryInfo("TestData");
        private static DirectoryInfo Input = new DirectoryInfo(Path.Combine(TestData.FullName, "Input"));
        private static DirectoryInfo Expected = new DirectoryInfo(Path.Combine(TestData.FullName, "Expected"));

        private FileInfo GetTempFile(string extension)
        {
            var dirname = System.IO.Path.GetTempPath();
            var filename = "XProc_" + Guid.NewGuid().ToString();
            var path = Path.Combine(dirname, filename);
            path = Path.ChangeExtension(path, extension);
            return new FileInfo(path);
        }

        private string GetText(FileInfo file)
        {
            using (var reader = file.OpenText())
            {
                return reader.ReadToEnd();
            }
        }

        [Test]
        public void EnsureTestDataAvailable()
        {
            Assert.IsTrue(TestData.Exists);
            Assert.IsTrue(Input.Exists);
            Assert.IsTrue(Expected.Exists);
        }

        [Test]
        public void EnsureBlankStylesheetDoesNothing()
        {
            var tran = XslTransformerFactory.GetTransform();
            tran.Load(Path.Combine(TestData.FullName, "blank.xsl"));
            var input = new FileInfo(Path.Combine(Input.FullName, "hello.xml"));
            var result = GetTempFile("xml");
            tran.Transform(input.FullName, result.FullName);
            Assert.AreEqual(input.Length, result.Length);
            Assert.AreEqual(GetText(input), GetText(result));
        }
    }
}
