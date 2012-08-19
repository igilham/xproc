using System;
using System.IO;
using IGilham.XProc.Core;
using NUnit.Framework;

namespace IGilham.XProc.UnitTest
{
    [TestFixture]
    public class ArgumentParserTest
    {
        [Test]
        public void ParseWithZeroArgsReturnsFalse()
        {
            var parser = new ArgumentParser();
            Assert.IsFalse(parser.Parse(), "Parse returned true");
        }

        [Test]
        public void ParseWithThreeArgsReturnsFalse()
        {
            var parser = new ArgumentParser();
            Assert.IsFalse(parser.Parse("app.exe", TestUtilities.Input.FullName, TestUtilities.Output.FullName), "Parse returned true");
        }

        [Test]
        public void ParseWithFakeDirectoryArgsReturnsFalse()
        {
            var parser = new ArgumentParser();
            Assert.IsFalse(parser.Parse("app.exe",
                Path.Combine(TestUtilities.Input.FullName, Guid.NewGuid().ToString()),
                Path.Combine(TestUtilities.Output.FullName, Guid.NewGuid().ToString()),
                TestUtilities.BlankXsl.FullName), "Parse returned true");
        }

        [Test]
        public void ParseWithCorrectArgs()
        {
            var parser = new ArgumentParser();
            Assert.IsTrue(parser.Parse("app.exe",
                TestUtilities.Input.FullName,
                TestUtilities.Output.FullName,
                TestUtilities.BlankXsl.FullName), "Parse returned false");
            Assert.AreEqual(TestUtilities.Input.FullName, parser.InputPath.FullName, "Input path set incorrectly");
            Assert.AreEqual(TestUtilities.Output.FullName, parser.OutputPath.FullName, "Output path set incorrectly");
            Assert.AreEqual(TestUtilities.BlankXsl.FullName, parser.Stylesheet.FullName, "stylesheet path set incorrectly");
        }
    }
}
