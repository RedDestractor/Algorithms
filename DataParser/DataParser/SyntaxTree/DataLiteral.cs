using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser.SyntaxTree
{
    class DataLiteral : IDataLiteral
    {
        public string Value { get; set; }

        public DataLiteral(string value)
        {
            Value = value;
        }
    }
}
