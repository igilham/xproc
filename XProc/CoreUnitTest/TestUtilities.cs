using System;
using System.IO;

namespace IGilham.XProc.UnitTest
{
    static class TestUtilities
    {
        public static DirectoryInfo TestData = new DirectoryInfo("TestData");
        public static DirectoryInfo Input = new DirectoryInfo(Path.Combine(TestData.FullName, "Input"));
        public static DirectoryInfo Catalog = new DirectoryInfo(Path.Combine(Input.FullName, "Catalog"));
        public static DirectoryInfo Expected = new DirectoryInfo(Path.Combine(TestData.FullName, "Expected"));
        public static DirectoryInfo Output = new DirectoryInfo(Path.Combine(TestData.FullName, "Output"));
        public static FileInfo BlankXsl = new FileInfo(Path.Combine(TestData.FullName, "blank.xsl"));
        public static FileInfo CatalogXsl = new FileInfo(Path.Combine(TestData.FullName, "catalog.xsl"));

        public static FileInfo GetTempFile(string extension)
        {
            var dirname = System.IO.Path.GetTempPath();
            var filename = "XProc_" + Guid.NewGuid().ToString();
            var path = Path.Combine(dirname, filename);
            path = Path.ChangeExtension(path, extension);
            return new FileInfo(path);
        }

        public static string GetText(FileInfo file)
        {
            using (var reader = file.OpenText())
            {
                return reader.ReadToEnd();
            }
        }
    }
}
