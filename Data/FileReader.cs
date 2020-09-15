using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Data
{
    public class FileReader : IFileReader
    {

        public List<string> ReadLines(string path, int? lineCount = null)
        {
            if (!File.Exists(path))
            {
                throw  new FileNotFoundException();
            }

            List<string> lines = lineCount == null
                ? File.ReadLines(path).ToList()
                : File.ReadLines(path).Take((int) lineCount).ToList();

            return lines;
        }
    }
}