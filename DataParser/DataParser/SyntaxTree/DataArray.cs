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
        public IDataLiteral Key { get; set; }

        public DataArray()
        {
            Elements = new List<IDataValue>();
        }

        public DataArray(IDataLiteral key, IEnumerable<IDataValue> elements)
        {
            Key = key;
            Elements = new List<IDataValue>();
            if (elements != null) foreach (var e in elements) Elements.Add(e);
        }
    }
}
