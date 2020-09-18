using System.Collections.Generic;

namespace Data
{
    public interface IDataProvider
    {
        List<string> GetRawData();
    }
}