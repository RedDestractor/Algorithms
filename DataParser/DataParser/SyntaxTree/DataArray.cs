using DataParser.SyntaxTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser
{
    class DataArray : IDataValue
    {
        public List<IDataValue> Elements { get; set; }

        public DataArray()
        {
            Elements = new List<IDataValue>();
        }

        public DataArray(IEnumerable<IDataValue> elements)
        {
            Elements = new List<IDataValue>();
            if (elements != null) foreach (var e in elements) Elements.Add(e);
        }
    }
}
