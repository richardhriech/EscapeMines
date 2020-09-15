using System.IO;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Data
{
    [TestClass]
    public class FileReaderTest
    {
        [TestMethod]
        public void ReadLines_WrongPath_ShouldThrowFileNotFoundException()
        {
            IFileReader fileReader = new FileReader();

            Assert.ThrowsException<FileNotFoundException>(() => fileReader.ReadLines(@"A:\ThisFileDoesntExist"));
        }
    }
}
