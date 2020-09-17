using System.Collections.Generic;
using System.IO;

namespace Data
{
    public interface IDataProvider
    {
        List<string> GetRawData();
    }
}