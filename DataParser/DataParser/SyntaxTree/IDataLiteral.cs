using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser.SyntaxTree
{
    public interface IDataLiteral : IDataValue
    {
        string Value { get; set; }
    }
}
