using System.Collections.Generic;
using System.IO;

namespace Data
{
    public interface IFileReader
    {
        List<string> ReadLines(string path, int? lineCount = null);
    }
}