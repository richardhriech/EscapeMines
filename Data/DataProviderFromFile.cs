
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Data
{
    public class DataProviderFromFile : IDataProvider
    {
        public DataProviderFromFile(string path)
        {
            Path = path;
        }

        public string Path { get; set; }

        public List<string> GetRawData()
        {
            if (!File.Exists(Path))
            {
                throw new FileNotFoundException("File not found on given path.");
            }

            List<string> lines = File.ReadLines(Path).ToList();

            return lines;
        }
    }
}