using System.IO;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Data
{
    [TestClass]
    public class DataProviderFromFileTest
    {
        [TestMethod]
        public void ReadLines_WrongPath_ShouldThrowFileNotFoundException()
        {
            IDataProvider dataProvider = new DataProviderFromFile(@"A:\ThisFileDoesntExist");

            Assert.ThrowsException<FileNotFoundException>(() => dataProvider.GetRawData());
        }
    }
}
