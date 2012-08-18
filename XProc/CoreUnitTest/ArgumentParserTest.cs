using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IGilham.Xproc;
using System.IO;

namespace IGilham.XProc.UnitTest
{
    [TestFixture]
    public class ArgumentParserTest
    {
        [Test]
        public void ParseWithZeroArgsReturnsFalse()
        {
            var parser = new ArgumentParser();
            Assert.IsFalse(parser.Parse());
        }

        [Test]
        public void ParseWithThreeArgsReturnsFalse()
        {
            var parser = new ArgumentParser();
            Assert.IsFalse(parser.Parse("app.exe", TestUtilities.Input.FullName, TestUtilities.Output.FullName));
        }

        [Test]
        public void ParseWithFakeDirectoryArgsReturnsFalse()
        {
            var parser = new ArgumentParser();
            Assert.IsFalse(parser.Parse("app.exe",
                Path.Combine(TestUtilities.Input.FullName, Guid.NewGuid().ToString()),
                Path.Combine(TestUtilities.Output.FullName, Guid.NewGuid().ToString()),
                TestUtilities.BlankXsl.FullName));
        }

        [Test]
        public void ParseWithCorrectArgs()
        {
            var parser = new ArgumentParser();
            Assert.IsTrue(parser.Parse("app.exe",
                TestUtilities.Input.FullName,
                TestUtilities.Output.FullName,
                TestUtilities.BlankXsl.FullName));
            Assert.AreEqual(TestUtilities.Input.FullName, parser.InputPath.FullName);
            Assert.AreEqual(TestUtilities.Output.FullName, parser.OutputPath.FullName);
            Assert.AreEqual(TestUtilities.BlankXsl.FullName, parser.Stylesheet.FullName);
        }
    }
}
